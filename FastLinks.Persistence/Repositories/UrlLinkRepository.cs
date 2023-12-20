using FastLinks.Application.Contracts.Persistence;
using FastLinks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastLinks.Persistence.Repositories;

public class UrlLinkRepository(FastLinksDbContext dbContext) : BaseRepository<UrlLink>(dbContext), IUrlLinkRepository
{
    public async Task<IReadOnlyList<UrlLink>> ListAllUserUrlLinksAsync(Guid UserId)
    {
        return await _dbContext.UrlLinks.Where(ul => ul.UserCreatorId == UserId).ToListAsync();
    }
}