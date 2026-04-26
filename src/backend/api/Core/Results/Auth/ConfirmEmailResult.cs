namespace api.Core.Results.Auth;

public sealed class ConfirmEmailResult
{
    private ConfirmEmailResult(bool succeeded, IReadOnlyDictionary<string, string[]> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public bool Succeeded { get; }

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public static ConfirmEmailResult Success() => new(true, new Dictionary<string, string[]>());

    public static ConfirmEmailResult Failure(string key, string error) =>
        new(false, new Dictionary<string, string[]> { [key] = [error] });
}
