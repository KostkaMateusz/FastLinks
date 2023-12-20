
using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkList;

public class GetUrlLinkListByUserQuery : IRequest<IReadOnlyList<GetUrlLinkListQueryVm>>
{
    public Guid UserId { get; set; }
    public GetUrlLinkListByUserQuery(Guid UserId)
    {
        this.UserId = UserId;
    }
}