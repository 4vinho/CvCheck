using api.App.Contracts.Auth;
using api.Core.Entities;
using api.Core.Interfaces.Auth;
using api.Core.Results.Auth;
using api.Infra.Auth;
using Microsoft.AspNetCore.Identity;

namespace api.Infra.Services.Auth;

public sealed class RegistrationService(UserManager<ApplicationUser> userManager) : IRegistrationService
{
    public async Task<RegistrationResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return RegistrationResult.Failure(IdentityErrorMapper.ToValidationErrors(createResult));
        }

        return RegistrationResult.Success(user.Email!);
    }
}
