using api.Core.Interfaces.Auth;

namespace backend.Tests.Auth;

public sealed class TestEmailConfirmationDeliveryService : IEmailConfirmationDeliveryService
{
    public List<EmailDelivery> Deliveries { get; } = [];

    public Task SendConfirmationCodeAsync(string email, string code, CancellationToken cancellationToken = default)
    {
        Deliveries.Add(new EmailDelivery(email, code));
        return Task.CompletedTask;
    }

    public sealed record EmailDelivery(string Email, string Code);
}
