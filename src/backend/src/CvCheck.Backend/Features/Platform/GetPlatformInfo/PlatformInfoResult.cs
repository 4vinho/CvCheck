namespace CvCheck.Backend.Features.Platform.GetPlatformInfo;

public sealed record PlatformInfoResult(
    string Name,
    string Environment,
    string Version);
