using FastLinks.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FastLinks.Identity;

public class FastLinksIdentityDbContext(DbContextOptions<FastLinksIdentityDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}