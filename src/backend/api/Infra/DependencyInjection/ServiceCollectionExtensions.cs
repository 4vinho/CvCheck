using api.Core.Entities;
using api.Core.Interfaces;
using api.Core.Interfaces.Auth;
using api.Infra.Data;
using api.Infra.Services.Auth;
using api.Infra.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Infra.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not configured.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddSingleton<IConfirmationCodeGenerator, RandomConfirmationCodeGenerator>();
        services.AddSingleton<IEmailConfirmationDeliveryService, NoOpEmailConfirmationDeliveryService>();
        services.AddSingleton<ITimeProvider, SystemTimeProvider>();

        return services;
    }
}
