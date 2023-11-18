using FastLinks.API.Extensions;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace FastLinks.API.Endpoints;

public sealed class UrlLinkRedirection : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app
            .MapGroup(this)
            .MapGet(RedirecteLinkToDestinationUrl, "{shortUrlAddress}");
    }

    public async Task<Ok<string>> RedirecteLinkToDestinationUrl(ISender sender, HttpResponse httpResponse , [FromRoute] string shortUrlAddress)
    {
        var getLinkDestination = new GetUrlLinkAddressQuery(shortUrlAddress);

        var urlLinkDestination = await sender.Send(getLinkDestination);
                
        return TypedResults.Ok(urlLinkDestination);
    }
}