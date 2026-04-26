namespace api.App.Contracts.Auth;

public sealed record ConfirmEmailResponse(
    string Email,
    string Status);
