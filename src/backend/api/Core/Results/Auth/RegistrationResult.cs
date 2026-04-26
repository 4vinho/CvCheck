namespace api.Core.Results.Auth;

public sealed class RegistrationResult
{
    private RegistrationResult(bool succeeded, string? email, IReadOnlyDictionary<string, string[]> errors)
    {
        Succeeded = succeeded;
        Email = email;
        Errors = errors;
    }

    public bool Succeeded { get; }

    public string? Email { get; }

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public static RegistrationResult Success(string email) =>
        new(true, email, new Dictionary<string, string[]>());

    public static RegistrationResult Failure(IReadOnlyDictionary<string, string[]> errors) =>
        new(false, null, errors);
}
