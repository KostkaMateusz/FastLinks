using FastLinks.Application.Contracts.Auth;
using FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;
using FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;
using FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FastLinks.API.Endpoints;

public static class AccountController
{
    public static void AddAccountController(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var accountGroup = endpointRouteBuilder.MapGroup($"api/{nameof(AccountController)}");

        accountGroup.MapGet("auth", AuthenticateAsync).WithSummary("Get Auth Token");
        accountGroup.MapPost("register", RegisterAsync).WithSummary("Create New User");

        accountGroup.WithTags(nameof(AccountController));
        accountGroup.WithOpenApi();
    }

    public static async Task<Ok<AuthenticationTokenQueryResponse>> AuthenticateAsync(ISender sender, [FromBody] AuthenticationTokenQuery request)
    {
        var userToken = await sender.Send(request);

        return TypedResults.Ok(userToken);
    }

    public static async Task<Ok<RegistrationRequestCommandResponse>> RegisterAsync(ISender sender, [FromBody] RegistrationRequestCommand request)
    {
        var registerResponse = await sender.Send(request);

        return TypedResults.Ok(registerResponse);
    }

    public static async Task<NoContent> DeleteCurrentUserAsync(ISender sender, IUser user)
    {
        var deleteUserCommand = new DeleteUserCommand(user.UserId);

        await sender.Send(deleteUserCommand);

        return TypedResults.NoContent();
    }
}