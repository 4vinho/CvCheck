using CvCheck.Application.Abstractions;
using CvCheck.Application.Configuration;
using CvCheck.Infrastructure.Platform;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CvCheck.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApiMetadataOptions>(configuration.GetSection(ApiMetadataOptions.SectionName));
        services.AddSingleton<IPlatformInfoProvider, PlatformInfoProvider>();

        return services;
    }
}
