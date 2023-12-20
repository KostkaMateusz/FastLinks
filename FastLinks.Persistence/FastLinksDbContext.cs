using FastLinks.Domain.Common;
using FastLinks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FastLinks.Persistence;

public class FastLinksDbContext(DbContextOptions<FastLinksDbContext> options) : DbContext(options)
{
    public DbSet<UrlLink> UrlLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
