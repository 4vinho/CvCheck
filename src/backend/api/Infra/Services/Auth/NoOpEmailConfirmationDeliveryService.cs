using api.Core.Interfaces.Auth;

namespace api.Infra.Services.Auth;

public sealed class NoOpEmailConfirmationDeliveryService : IEmailConfirmationDeliveryService
{
    public Task SendConfirmationCodeAsync(string email, string code, CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}
