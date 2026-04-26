using api.Core.Entities;
using api.Core.Interfaces;
using api.Core.Interfaces.Auth;
using api.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace backend.Tests.Auth;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string databaseName = $"cvcheck-tests-{Guid.NewGuid():N}";

    public TestConfirmationCodeGenerator CodeGenerator { get; } = new();

    public TestEmailConfirmationDeliveryService EmailDeliveryService { get; } = new();

    public MutableTimeProvider TimeProvider { get; } = new(new DateTimeOffset(2026, 4, 26, 12, 0, 0, TimeSpan.Zero));

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureLogging(logging => logging.ClearProviders());

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.RemoveAll<IDbContextOptionsConfiguration<ApplicationDbContext>>();
            services.RemoveAll<IConfirmationCodeGenerator>();
            services.RemoveAll<IEmailConfirmationDeliveryService>();
            services.RemoveAll<ITimeProvider>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(databaseName));

            services.AddSingleton<ITimeProvider>(TimeProvider);
            services.AddSingleton<IConfirmationCodeGenerator>(CodeGenerator);
            services.AddSingleton<IEmailConfirmationDeliveryService>(EmailDeliveryService);

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        });
    }

    public async Task<ApplicationUser?> FindUserByEmailAsync(string email)
    {
        using var scope = Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<List<EmailConfirmationCode>> GetCodesForUserAsync(string email)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = await FindUserByEmailAsync(email);
        if (user is null)
        {
            return [];
        }

        return await dbContext.EmailConfirmationCodes
            .Where(code => code.UserId == user.Id)
            .OrderBy(code => code.CreatedAtUtc)
            .ToListAsync();
    }
}
