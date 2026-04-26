using api.App.Contracts.Auth;
using api.Core.Entities;
using api.Core.Interfaces.Auth;
using api.Core.Results.Auth;
using Microsoft.AspNetCore.Identity;

namespace api.Infra.Services.Auth;

public sealed class LoginService(UserManager<ApplicationUser> userManager) : ILoginService
{
    public async Task<LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return LoginResult.Failure("email", "Invalid credentials.");
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return LoginResult.Failure("email", "Invalid credentials.");
        }

        if (!user.EmailConfirmed)
        {
            return LoginResult.PendingEmailConfirmation(user.Email!);
        }

        return LoginResult.Authenticated(user.Email!);
    }
}
