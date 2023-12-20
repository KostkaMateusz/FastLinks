using FastLinks.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastLinks.Identity.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly FastLinksIdentityDbContext _identityDbContext;

    public UsersRepository(FastLinksIdentityDbContext identityDbContext)
    {
        _identityDbContext = identityDbContext;
    }

    public async Task<ApplicationUser?> GetApplicationUserByEmail(string email)
    {
        return await _identityDbContext.ApplicationUsers.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<ApplicationUser?> GetApplicationUserById(Guid userId)
    {
        return await _identityDbContext.ApplicationUsers.FirstOrDefaultAsync(user => user.UserId == userId);
    }

    public async Task<bool> ApplicationUserWithEmailExist(string email)
    {
        return await _identityDbContext.ApplicationUsers.Where(user => user.Email == email).AnyAsync();
    }

    public async Task<Guid> SaveNewUser(ApplicationUser applicationUser)
    {
        await _identityDbContext.ApplicationUsers.AddAsync(applicationUser);
        await _identityDbContext.SaveChangesAsync();

        return applicationUser.UserId;
    }

    public async Task DeleteUser(ApplicationUser applicationUser)
    {
        _identityDbContext.ApplicationUsers.Remove(applicationUser);
        await _identityDbContext.SaveChangesAsync();
    }
}
