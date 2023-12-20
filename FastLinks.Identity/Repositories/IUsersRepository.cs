using FastLinks.Identity.Entities;

namespace FastLinks.Identity.Repositories;

public interface IUsersRepository
{
    Task<bool> ApplicationUserWithEmailExist(string email);
    Task<ApplicationUser?> GetApplicationUserByEmail(string email);
    Task<Guid> SaveNewUser(ApplicationUser applicationUser);
}