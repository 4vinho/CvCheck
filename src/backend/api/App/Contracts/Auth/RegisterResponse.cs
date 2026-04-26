namespace api.App.Contracts.Auth;

public sealed record RegisterResponse(
    string Email,
    bool RequiresEmailConfirmation,
    string Status);
