using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;

public class GetUrlLinkQuery : IRequest<UrlLinkVm>
{
    public required string ShortUrl { get; set; }
}
