using FastLinks.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastLinks.Identity;

public class FastLinksIdentityDbContext : DbContext 
{
    public FastLinksIdentityDbContext(DbContextOptions<FastLinksIdentityDbContext> options) : base(options) {  }

    public DbSet<ApplicationUser> ApplicationUsers {  get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().HasKey(applicationUser => applicationUser.UserId);
    }
}