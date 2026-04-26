using Microsoft.AspNetCore.Identity;

namespace api.Infra.Auth;

public static class IdentityErrorMapper
{
    public static IReadOnlyDictionary<string, string[]> ToValidationErrors(IdentityResult identityResult)
    {
        var groupedErrors = identityResult.Errors
            .GroupBy(GetFieldName, StringComparer.Ordinal)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Description).Distinct(StringComparer.Ordinal).ToArray(),
                StringComparer.Ordinal);

        return groupedErrors;
    }

    private static string GetFieldName(IdentityError error) =>
        error.Code switch
        {
            nameof(IdentityErrorDescriber.InvalidEmail) => "email",
            nameof(IdentityErrorDescriber.DuplicateEmail) => "email",
            nameof(IdentityErrorDescriber.DuplicateUserName) => "email",
            nameof(IdentityErrorDescriber.InvalidUserName) => "email",
            nameof(IdentityErrorDescriber.PasswordMismatch) => "confirmPassword",
            nameof(IdentityErrorDescriber.PasswordRequiresDigit) => "password",
            nameof(IdentityErrorDescriber.PasswordRequiresLower) => "password",
            nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric) => "password",
            nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars) => "password",
            nameof(IdentityErrorDescriber.PasswordRequiresUpper) => "password",
            nameof(IdentityErrorDescriber.PasswordTooShort) => "password",
            _ => string.Empty
        };
}
