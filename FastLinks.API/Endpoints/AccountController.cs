using FastLinks.Application.Contracts.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using FastLinks.API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FastLinks.API.Endpoints;

public static class AccountController
{
    public static void AddAccountController(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var accountGroup = endpointRouteBuilder.MapGroup($"api/{nameof(AccountController)}");

        accountGroup.MapGet("authenticate", AuthenticateAsync);
        accountGroup.MapGet("register", RegisterAsync);

        accountGroup.WithTags(nameof(AccountController));
        accountGroup.WithOpenApi();     
    }

    public static async Task<Ok<AuthenticationResponse>> AuthenticateAsync(IAuthenticationService authenticationService,[FromBody] AuthenticationRequest request)
    {
        return TypedResults.Ok(await authenticationService.AuthenticateAsync(request));
    }

    public static async Task<Ok<RegistrationResponse>> RegisterAsync(IAuthenticationService authenticationService, [FromBody] RegistrationRequest request)
    {
        return TypedResults.Ok(await authenticationService.RegisterAsync(request));
    }
}