namespace api.Core.Interfaces.Auth;

public interface IEmailConfirmationDeliveryService
{
    Task SendConfirmationCodeAsync(string email, string code, CancellationToken cancellationToken = default);
}
