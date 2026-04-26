namespace api.Core.Entities;

public sealed class EmailConfirmationCode
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset ExpiresAtUtc { get; set; }

    public DateTimeOffset? ConsumedAtUtc { get; set; }

    public DateTimeOffset? InvalidatedAtUtc { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
