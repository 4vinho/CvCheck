using api.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Infra.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (!dbContext.Database.IsRelational())
        {
            await dbContext.Database.EnsureCreatedAsync();
            return;
        }

        await dbContext.Database.MigrateAsync();
    }
}
