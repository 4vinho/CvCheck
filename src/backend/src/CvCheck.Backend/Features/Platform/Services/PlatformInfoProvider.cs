using System.Reflection;
using CvCheck.Backend.Abstractions;
using CvCheck.Backend.Configuration;
using CvCheck.Backend.Features.Platform.GetPlatformInfo;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CvCheck.Backend.Features.Platform.Services;

public sealed class PlatformInfoProvider(
    IHostEnvironment hostEnvironment,
    IOptions<ApiMetadataOptions> apiMetadataOptions) : IPlatformInfoProvider
{
    public Task<PlatformInfoResult> GetAsync(CancellationToken cancellationToken)
    {
        var assemblyVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? apiMetadataOptions.Value.Version;

        var result = new PlatformInfoResult(
            apiMetadataOptions.Value.Name,
            hostEnvironment.EnvironmentName,
            assemblyVersion);

        return Task.FromResult(result);
    }
}
