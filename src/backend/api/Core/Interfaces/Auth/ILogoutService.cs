namespace api.Core.Interfaces.Auth;

public interface ILogoutService
{
    Task LogoutAsync(CancellationToken cancellationToken = default);
}
