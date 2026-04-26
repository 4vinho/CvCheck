using api.App.Contracts.Auth;
using api.Core.Results.Auth;

namespace api.Core.Interfaces.Auth;

public interface ILoginService
{
    Task<LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
