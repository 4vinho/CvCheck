using System.ComponentModel.DataAnnotations;

namespace api.App.Contracts.Auth;

public sealed class ResendEmailConfirmationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;
}
