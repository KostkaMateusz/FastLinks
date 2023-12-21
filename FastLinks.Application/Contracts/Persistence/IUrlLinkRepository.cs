using FastLinks.Domain.Entities;

namespace FastLinks.Application.Contracts.Persistence;

public interface IUrlLinkRepository : IAsyncRepository<UrlLink>
{
    Task<IReadOnlyList<UrlLink>> ListAllUserUrlLinksAsync(Guid UserId);
    Task DeleteLinks(IList<UrlLink> urlLinks);
}