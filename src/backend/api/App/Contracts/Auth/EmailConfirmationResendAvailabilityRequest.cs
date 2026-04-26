using System.ComponentModel.DataAnnotations;

namespace api.App.Contracts.Auth;

public sealed class EmailConfirmationResendAvailabilityRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;
}
