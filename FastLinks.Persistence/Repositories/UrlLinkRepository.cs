

using FastLinks.Application.Contracts.Persistence;
using FastLinks.Domain.Entities;

namespace FastLinks.Persistence.Repositories;

public class UrlLinkRepository : BaseRepository<UrlLink>, IUrlLinkRepository
{
    public UrlLinkRepository(FastLinksDbContext dbContext) : base(dbContext)
    {        
    }


}
