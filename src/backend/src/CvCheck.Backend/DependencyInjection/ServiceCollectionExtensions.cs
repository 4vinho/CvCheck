using CvCheck.Backend.Abstractions;
using CvCheck.Backend.Common.Behaviors;
using CvCheck.Backend.Configuration;
using CvCheck.Backend.Features.Platform.Services;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CvCheck.Backend.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackendServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApiMetadataOptions>(configuration.GetSection(ApiMetadataOptions.SectionName));

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddSingleton<IPlatformInfoProvider, PlatformInfoProvider>();

        return services;
    }
}
