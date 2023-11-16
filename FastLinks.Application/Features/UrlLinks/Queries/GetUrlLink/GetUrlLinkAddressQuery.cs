using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;

public class GetUrlLinkAddressQuery : IRequest<string>
{
    public required string ShortUrl { get; set; }
}