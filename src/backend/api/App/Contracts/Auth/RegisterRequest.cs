using System.ComponentModel.DataAnnotations;

namespace api.App.Contracts.Auth;

public sealed class RegisterRequest : IValidatableObject
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;

    [Required]
    public string ConfirmPassword { get; init; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.Equals(Password, ConfirmPassword, StringComparison.Ordinal))
        {
            yield return new ValidationResult(
                "Password and confirmation password do not match.",
                [nameof(ConfirmPassword)]);
        }
    }
}
