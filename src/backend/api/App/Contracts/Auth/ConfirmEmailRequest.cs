using System.ComponentModel.DataAnnotations;

namespace api.App.Contracts.Auth;

public sealed class ConfirmEmailRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [Length(6, 6)]
    public string Code { get; init; } = string.Empty;
}
