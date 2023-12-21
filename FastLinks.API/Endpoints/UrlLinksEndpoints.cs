using FastLinks.API.Models;
using FastLinks.Application.Contracts.Auth;
using FastLinks.Application.Features.UrlLinks.Commands.CreateUrlLink;
using FastLinks.Application.Features.UrlLinks.Commands.DeleteUrlLink;
using FastLinks.Application.Features.UrlLinks.Commands.UpdateUrlLink;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetail;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkList;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FastLinks.API.Endpoints;

public static class UrlLinksEndpoints
{
    public static void AddUrlLinksEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var urlLinksGroup = endpointRouteBuilder.MapGroup($"api/{nameof(UrlLinksEndpoints)}");

        urlLinksGroup.MapPost("", CreateLink).WithSummary("Create new short link");
        urlLinksGroup.MapDelete("{shortUrlAddress}", DeleteLink).WithSummary("Delete Short Link");
        urlLinksGroup.MapPut("{shortUrlAddress}", UpdateLink).WithSummary("Update Link");
        urlLinksGroup.MapGet("{shortUrlAddress}", GetLinkDetails).WithName(nameof(GetLinkDetails)).WithSummary("Get short link details");
        urlLinksGroup.MapGet("", GetLinkDetailsList).WithSummary("Get List of short links");

        urlLinksGroup.RequireAuthorization();
        urlLinksGroup.WithTags(nameof(UrlLinksEndpoints));
        urlLinksGroup.WithOpenApi();
    }

    public static async Task<CreatedAtRoute<CreateLinkCommandResponse>> CreateLink(ISender sender, IUser user, [FromBody] CreateLinkDto command)
    {
        var createLinkCommand = new CreateLinkCommand() { UrlAddress = command.UrlAddress, UserCreatorId = user.UserId };

        var newLinkShortUrl = await sender.Send(createLinkCommand);

        return TypedResults.CreatedAtRoute(newLinkShortUrl, nameof(GetLinkDetails), new { shortUrlAddress = newLinkShortUrl });
    }

    public static async Task<IResult> DeleteLink(ISender sender, IUser user, [FromRoute] string shortUrlAddress)
    {
        var deleteLinkCommand = new DeleteLinkCommand(shortUrlAddress, user.UserId);

        await sender.Send(deleteLinkCommand);

        return TypedResults.NoContent();
    }

    public static async Task<IResult> UpdateLink(ISender sender, IUser user, [FromRoute] string shortUrlAddress, [FromBody] UpdateLinkDto updateLinkDto)
    {
        var updateLinkCommand = new UpdateLinkCommand(shortUrlAddress, updateLinkDto.ExpirationDate, user.UserId);

        var shortUrl = await sender.Send(updateLinkCommand);

        return TypedResults.Ok(shortUrl);
    }

    public static async Task<Ok<GetUrlLinkDetailsQueryVm>> GetLinkDetails(ISender sender, IUser user, [FromRoute] string shortUrlAddress)
    {
        var getUrlLinkQuery = new GetUrlLinkDetailsQuery(shortUrlAddress);

        var shortUrlLinkDetails = await sender.Send(getUrlLinkQuery);

        return TypedResults.Ok(shortUrlLinkDetails);
    }

    public static async Task<Ok<IReadOnlyList<GetUrlLinkListQueryVm>>> GetLinkDetailsList(ISender sender, IUser user)
    {
        var getUrlLinkQueryList = new GetUrlLinkListByUserQuery(user.UserId);

        var shortUrlLinkDetailsList = await sender.Send(getUrlLinkQueryList);

        return TypedResults.Ok(shortUrlLinkDetailsList);
    }
}