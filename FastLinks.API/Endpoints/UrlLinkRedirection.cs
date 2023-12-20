using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLink;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FastLinks.API.Endpoints;

public static class UrlLinkRedirection
{
    public static void AddUrlLinkRedirection(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var urlLinksGroup = endpointRouteBuilder.MapGroup($"api/{nameof(UrlLinkRedirection)}");

        urlLinksGroup.MapGet("{shortUrlAddress}", RedirecteLinkToDestinationUrl).WithSummary("Get original Link");

        urlLinksGroup.WithTags(nameof(UrlLinkRedirection));
        urlLinksGroup.WithOpenApi();
    }

    public static async Task<Ok<string>> RedirecteLinkToDestinationUrl(ISender sender, HttpResponse httpResponse, [FromRoute] string shortUrlAddress)
    {
        var getLinkDestination = new GetUrlLinkAddressQuery(shortUrlAddress);

        var urlLinkDestination = await sender.Send(getLinkDestination);

        return TypedResults.Ok(urlLinkDestination);
    }
}