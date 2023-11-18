using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetail;

public class GetUrlLinkDetailsQuery : IRequest<GetUrlLinkDetailsQueryVm>
{
    public string ShortUrl { get; set; }

    public GetUrlLinkDetailsQuery(string ShortUrl)
    {
        this.ShortUrl = ShortUrl;
    }
}
