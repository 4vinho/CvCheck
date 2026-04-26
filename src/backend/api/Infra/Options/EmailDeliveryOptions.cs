namespace api.Infra.Options;

public sealed class EmailDeliveryOptions
{
    public const string SectionName = "EmailDelivery";

    public string FromAddress { get; init; } = "noreply@cvcheck.local";

    public string FromName { get; init; } = "CvCheck";

    public string SmtpHost { get; init; } = "localhost";

    public int SmtpPort { get; init; } = 1025;

    public bool UseSsl { get; init; }
}
