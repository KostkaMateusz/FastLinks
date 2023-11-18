using FastLinks.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FastLinks.Identity;

public class FastLinksIdentityDbContext(DbContextOptions<FastLinksIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options) { }