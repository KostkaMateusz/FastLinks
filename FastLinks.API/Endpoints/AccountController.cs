using FastLinks.Application.Contracts.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using FastLinks.API.Extensions;

namespace FastLinks.API.Endpoints;

public sealed class AccountController : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app
            .MapGroup(this)
            .MapPost(AuthenticateAsync, "/authenticate")
            .MapPost(RegisterAsync, "/register");
    }

    public async Task<Ok<AuthenticationResponse>> AuthenticateAsync(IAuthenticationService authenticationService, AuthenticationRequest request)
    {
        return TypedResults.Ok(await authenticationService.AuthenticateAsync(request));
    }

    public async Task<Ok<RegistrationResponse>> RegisterAsync(IAuthenticationService authenticationService, RegistrationRequest request)
    {
        return TypedResults.Ok(await authenticationService.RegisterAsync(request));
    }
}