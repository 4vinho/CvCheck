using api.Core.Entities;
using api.Core.Results.Auth;

namespace api.Core.Interfaces.Auth;

public interface IEmailConfirmationService
{
    Task GenerateAndSendCodeAsync(ApplicationUser user, CancellationToken cancellationToken = default);

    Task<ResendEmailConfirmationResult> ResendAsync(string email, CancellationToken cancellationToken = default);

    Task<ConfirmEmailResult> ConfirmAsync(string email, string code, CancellationToken cancellationToken = default);
}
