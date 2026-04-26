namespace api.App.Contracts.Auth;

public sealed record ResendEmailConfirmationResponse(
    string Email,
    string Status);
