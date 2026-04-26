namespace api.App.Contracts.Auth;

public sealed record EmailConfirmationResendAvailabilityResponse(
    string Email,
    bool CanResend,
    int RemainingSeconds);
