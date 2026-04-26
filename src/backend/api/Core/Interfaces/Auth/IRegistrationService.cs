using api.App.Contracts.Auth;
using api.Core.Results.Auth;

namespace api.Core.Interfaces.Auth;

public interface IRegistrationService
{
    Task<RegistrationResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
}
