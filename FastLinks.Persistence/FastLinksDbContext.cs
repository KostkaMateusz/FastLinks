using FastLinks.Domain.Entities;
using FastLinks.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FastLinks.Persistence;

public class FastLinksDbContext : IdentityDbContext<ApplicationUser>
{
    public FastLinksDbContext(DbContextOptions<FastLinksDbContext> options) : base(options) { }

    public DbSet<UrlLink> UrlLinks => Set<UrlLink>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
