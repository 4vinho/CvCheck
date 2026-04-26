namespace api.Core.Results.Auth;

public sealed class LoginResult
{
    private LoginResult(bool succeeded, bool requiresEmailConfirmation, string? email, IReadOnlyDictionary<string, string[]> errors)
    {
        Succeeded = succeeded;
        RequiresEmailConfirmation = requiresEmailConfirmation;
        Email = email;
        Errors = errors;
    }

    public bool Succeeded { get; }

    public bool RequiresEmailConfirmation { get; }

    public string? Email { get; }

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public static LoginResult Authenticated(string email) =>
        new(true, false, email, new Dictionary<string, string[]>());

    public static LoginResult PendingEmailConfirmation(string email) =>
        new(false, true, email, new Dictionary<string, string[]>());

    public static LoginResult Failure(string key, string error) =>
        new(false, false, null, new Dictionary<string, string[]> { [key] = [error] });
}
