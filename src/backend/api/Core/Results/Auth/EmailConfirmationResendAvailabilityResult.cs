namespace api.Core.Results.Auth;

public sealed class EmailConfirmationResendAvailabilityResult
{
    private EmailConfirmationResendAvailabilityResult(
        bool succeeded,
        bool canResend,
        int remainingSeconds,
        IReadOnlyDictionary<string, string[]> errors)
    {
        Succeeded = succeeded;
        CanResend = canResend;
        RemainingSeconds = remainingSeconds;
        Errors = errors;
    }

    public bool Succeeded { get; }

    public bool CanResend { get; }

    public int RemainingSeconds { get; }

    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public static EmailConfirmationResendAvailabilityResult Success(bool canResend, int remainingSeconds) =>
        new(true, canResend, remainingSeconds, new Dictionary<string, string[]>());

    public static EmailConfirmationResendAvailabilityResult Failure(string key, string error) =>
        new(false, false, 0, new Dictionary<string, string[]> { [key] = [error] });
}
