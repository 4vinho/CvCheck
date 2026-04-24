namespace CvCheck.Api.Contracts;

public sealed record PlatformInfoResponse(
    string Name,
    string Environment,
    string Version);
