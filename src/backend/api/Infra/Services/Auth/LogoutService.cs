using api.Core.Entities;
using api.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Identity;

namespace api.Infra.Services.Auth;

public sealed class LogoutService(SignInManager<ApplicationUser> signInManager) : ILogoutService
{
    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await signInManager.SignOutAsync();
    }
}
