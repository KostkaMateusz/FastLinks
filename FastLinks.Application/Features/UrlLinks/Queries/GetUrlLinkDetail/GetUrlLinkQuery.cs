using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetails;

public class GetUrlLinkQuery : IRequest<UrlLinkAddressVm>
{
    public required string ShortUrl { get; set; }
}
