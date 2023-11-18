using FastLinks.API.Extensions;
using FastLinks.API.Models;
using FastLinks.Application.Contracts.Identity;
using FastLinks.Application.Features.UrlLinks.Commands.CreateUrlLink;
using FastLinks.Application.Features.UrlLinks.Commands.DeleteUrlLink;
using FastLinks.Application.Features.UrlLinks.Commands.UpdateUrlLink;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetails;
using FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkList;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FastLinks.API.Endpoints;

public sealed class UrlLinks : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app
            .MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateLink)
            .MapDelete(DeleteLink, "{shortUrlAddress}")
            .MapPut(UpdateLink, "{shortUrlAddress}")
            .MapGet(GetLinkDetails, "{shortUrlAddress}")
            .MapGet(GetLinkDetailsList);
    }

    public async Task<Created<CreateLinkCommandResponse>> CreateLink(ISender sender, IUser user, [FromBody] CreateLinkDto command)
    {
        var createLinkCommand = new CreateLinkCommand() { UrlAddress = command.UrlAddress, UserCreatorId = user.UserId };

        var newLinkShortUrl = await sender.Send(createLinkCommand);

        return TypedResults.Created("", newLinkShortUrl);
    }

    public async Task<IResult> DeleteLink(ISender sender, IUser user, [FromRoute] string shortUrlAddress)
    {
        var deleteLinkCommand = new DeleteLinkCommand(shortUrlAddress, user.UserId);

        await sender.Send(deleteLinkCommand);

        return TypedResults.NoContent();
    }

    public async Task<IResult> UpdateLink(ISender sender, IUser user, [FromRoute] string shortUrlAddress, [FromBody] UpdateLinkDto updateLinkDto)
    {
        var updateLinkCommand = new UpdateLinkCommand(shortUrlAddress, updateLinkDto.ExpirationDate , user.UserId);

        var shortUrl = await sender.Send(updateLinkCommand);

        return TypedResults.Ok(shortUrl);
    }

    public async Task<Ok<GetUrlLinkDetailsQueryVm>> GetLinkDetails(ISender sender, IUser user, [FromRoute] string shortUrlAddress)
    {
        var getUrlLinkQuery = new GetUrlLinkDetailsQuery(shortUrlAddress);

        var shortUrlLinkDetails = await sender.Send(getUrlLinkQuery);

        return TypedResults.Ok(shortUrlLinkDetails);
    }

    public async Task<Ok<IReadOnlyList<GetUrlLinkListQueryVm>>> GetLinkDetailsList(ISender sender, IUser user)
    {
        var getUrlLinkQueryList = new GetUrlLinkListByUserQuery(user.UserId);

        var shortUrlLinkDetailsList = await sender.Send(getUrlLinkQueryList);

        return TypedResults.Ok(shortUrlLinkDetailsList);
    }
}