namespace api.App.Contracts.Auth;

public sealed record LoginResponse(
    string Email,
    bool RequiresEmailConfirmation,
    string Status);
