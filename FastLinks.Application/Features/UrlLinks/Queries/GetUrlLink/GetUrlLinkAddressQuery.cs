using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;

public class GetUrlLinkAddressQuery : IRequest<string>
{
    public string ShortUrl { get; set; }

    public GetUrlLinkAddressQuery(string ShortUrl)
    {
        this.ShortUrl = ShortUrl;
    }
}