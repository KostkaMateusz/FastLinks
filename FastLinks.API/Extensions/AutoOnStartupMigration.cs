using FastLinks.Identity;
using FastLinks.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FastLinks.API.Extensions;

public static class AutoOnStartupMigration
{
    public static async Task EnsureDbCreated(IServiceProvider services)
    {
        using var db = services.CreateScope().ServiceProvider.GetRequiredService<FastLinksDbContext>();
        await db.Database.MigrateAsync();

        using var identityDb = services.CreateScope().ServiceProvider.GetRequiredService<FastLinksIdentityDbContext>();
        await identityDb.Database.MigrateAsync();
    }
}
