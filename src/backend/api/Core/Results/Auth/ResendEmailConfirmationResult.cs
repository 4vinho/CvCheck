namespace api.Core.Results.Auth;

public sealed class ResendEmailConfirmationResult
{
    private ResendEmailConfirmationResult(bool succeeded, IReadOnlyDictionary<string, string[]> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public bool Succeeded { get; }

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public static ResendEmailConfirmationResult Success() => new(true, new Dictionary<string, string[]>());

    public static ResendEmailConfirmationResult Failure(string key, string error) =>
        new(false, new Dictionary<string, string[]> { [key] = [error] });
}
