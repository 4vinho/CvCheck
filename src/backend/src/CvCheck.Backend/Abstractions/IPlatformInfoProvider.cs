using CvCheck.Backend.Features.Platform.GetPlatformInfo;

namespace CvCheck.Backend.Abstractions;

public interface IPlatformInfoProvider
{
    Task<PlatformInfoResult> GetAsync(CancellationToken cancellationToken);
}
