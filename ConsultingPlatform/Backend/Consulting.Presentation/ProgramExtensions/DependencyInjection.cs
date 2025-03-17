using Consulting.Infrastructure.db;
using Consulting.Infrastructure.db.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Consulting.Presentation.ProgramExtensions;

public static class DependencyInjection
{
    public static async Task ApplyMigrationsAndSeedAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<AppDbContext>();

        try
        {
            //if (dbContext.Database.GetPendingMigrations() != null)
            //{
            //    await dbContext.Database.EnsureCreatedAsync();
            //}

            await dbContext.Database.MigrateAsync();
            await IdentitySeeder.SeedAsync(services);
        }
        catch (Exception)
        {
            // TODO: Add logging
            throw;
        }
    }
}
