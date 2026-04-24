using System.Reflection;
using CvCheck.Application.Abstractions;
using CvCheck.Application.Configuration;
using CvCheck.Application.Platform.GetPlatformInfo;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CvCheck.Infrastructure.Platform;

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
