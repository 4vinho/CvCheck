using System.Net.Mail;
using api.Core.Interfaces.Auth;
using api.Infra.Options;
using Microsoft.Extensions.Options;

namespace api.Infra.Services.Auth;

public sealed class SmtpEmailConfirmationDeliveryService(
    IOptions<EmailDeliveryOptions> options) : IEmailConfirmationDeliveryService
{
    private readonly EmailDeliveryOptions emailOptions = options.Value;

    public async Task SendConfirmationCodeAsync(string email, string code, CancellationToken cancellationToken = default)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(emailOptions.FromAddress, emailOptions.FromName),
            Subject = "Confirme seu email",
            Body = $"Seu codigo de confirmacao e: {code}",
            IsBodyHtml = false
        };

        message.To.Add(email);

        using var client = new SmtpClient(emailOptions.SmtpHost, emailOptions.SmtpPort)
        {
            EnableSsl = emailOptions.UseSsl
        };

        cancellationToken.ThrowIfCancellationRequested();
        await client.SendMailAsync(message, cancellationToken);
    }
}
