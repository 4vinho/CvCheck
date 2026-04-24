using CvCheck.Application.Platform.GetPlatformInfo;

namespace CvCheck.Application.Abstractions;

public interface IPlatformInfoProvider
{
    Task<PlatformInfoResult> GetAsync(CancellationToken cancellationToken);
}
