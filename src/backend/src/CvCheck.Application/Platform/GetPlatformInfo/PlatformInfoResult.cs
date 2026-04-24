namespace CvCheck.Application.Platform.GetPlatformInfo;

public sealed record PlatformInfoResult(
    string Name,
    string Environment,
    string Version);
