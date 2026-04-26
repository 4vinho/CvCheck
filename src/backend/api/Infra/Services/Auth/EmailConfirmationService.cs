using api.Core.Entities;
using api.Core.Interfaces;
using api.Core.Interfaces.Auth;
using api.Core.Results.Auth;
using api.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Infra.Services.Auth;

public sealed class EmailConfirmationService(
    ApplicationDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    IConfirmationCodeGenerator confirmationCodeGenerator,
    IEmailConfirmationDeliveryService deliveryService,
    ITimeProvider timeProvider) : IEmailConfirmationService
{
    private static readonly TimeSpan CodeLifetime = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan ResendCooldown = TimeSpan.FromMinutes(1);

    public async Task GenerateAndSendCodeAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var now = timeProvider.UtcNow;
        var activeCodes = await dbContext.EmailConfirmationCodes
            .Where(code => code.UserId == user.Id && code.ConsumedAtUtc == null && code.InvalidatedAtUtc == null)
            .ToListAsync(cancellationToken);

        foreach (var activeCode in activeCodes)
        {
            activeCode.InvalidatedAtUtc = now;
        }

        var code = new EmailConfirmationCode
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Code = confirmationCodeGenerator.GenerateCode(),
            CreatedAtUtc = now,
            ExpiresAtUtc = now.Add(CodeLifetime)
        };

        dbContext.EmailConfirmationCodes.Add(code);
        await dbContext.SaveChangesAsync(cancellationToken);
        await deliveryService.SendConfirmationCodeAsync(user.Email!, code.Code, cancellationToken);
    }

    public async Task<ResendEmailConfirmationResult> ResendAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return ResendEmailConfirmationResult.Failure("email", "Account was not found.");
        }

        if (user.EmailConfirmed)
        {
            return ResendEmailConfirmationResult.Failure("email", "Email is already confirmed.");
        }

        var now = timeProvider.UtcNow;
        var latestActiveCode = await dbContext.EmailConfirmationCodes
            .Where(code => code.UserId == user.Id && code.ConsumedAtUtc == null && code.InvalidatedAtUtc == null)
            .OrderByDescending(code => code.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (latestActiveCode is not null && now - latestActiveCode.CreatedAtUtc < ResendCooldown)
        {
            return ResendEmailConfirmationResult.Failure("email", "Wait before requesting a new confirmation code.");
        }

        await GenerateAndSendCodeAsync(user, cancellationToken);
        return ResendEmailConfirmationResult.Success();
    }

    public async Task<ConfirmEmailResult> ConfirmAsync(string email, string code, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return ConfirmEmailResult.Failure("email", "Account was not found.");
        }

        if (user.EmailConfirmed)
        {
            return ConfirmEmailResult.Failure("code", "Email is already confirmed.");
        }

        var storedCode = await dbContext.EmailConfirmationCodes
            .Where(candidate => candidate.UserId == user.Id && candidate.Code == code)
            .OrderByDescending(candidate => candidate.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (storedCode is null)
        {
            return ConfirmEmailResult.Failure("code", "Confirmation code is invalid.");
        }

        var now = timeProvider.UtcNow;
        if (storedCode.InvalidatedAtUtc is not null || storedCode.ConsumedAtUtc is not null)
        {
            return ConfirmEmailResult.Failure("code", "Confirmation code is no longer valid.");
        }

        if (storedCode.ExpiresAtUtc <= now)
        {
            return ConfirmEmailResult.Failure("code", "Confirmation code has expired.");
        }

        storedCode.ConsumedAtUtc = now;
        user.EmailConfirmed = true;

        await userManager.UpdateAsync(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return ConfirmEmailResult.Success();
    }
}
