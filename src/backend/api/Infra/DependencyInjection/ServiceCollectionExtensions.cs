using api.Core.Entities;
using api.Core.Interfaces;
using api.Core.Interfaces.Auth;
using api.Infra.Data;
using api.Infra.Options;
using api.Infra.Services.Auth;
using api.Infra.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        services.Configure<EmailDeliveryOptions>(
            configuration.GetSection(EmailDeliveryOptions.SectionName));

        services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddSignInManager()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthorization();
        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });

        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ILogoutService, LogoutService>();
        services.AddSingleton<IConfirmationCodeGenerator, RandomConfirmationCodeGenerator>();
        services.AddSingleton<IEmailConfirmationDeliveryService, SmtpEmailConfirmationDeliveryService>();
        services.AddSingleton<ITimeProvider, SystemTimeProvider>();

        return services;
    }
}
