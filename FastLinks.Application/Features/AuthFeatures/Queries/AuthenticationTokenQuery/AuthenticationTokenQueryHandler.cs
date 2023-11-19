using FastLinks.Application.Contracts.Auth;
using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;

public class AuthenticationTokenQueryHandler : IRequestHandler<AuthenticationTokenQuery, AuthenticationTokenQueryResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationTokenQueryHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<AuthenticationTokenQueryResponse> Handle(AuthenticationTokenQuery request, CancellationToken cancellationToken)
    {
        var validator = new AuthenticationTokenQueryValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var usetToken = await _authenticationService.AuthenticateAsync(request);

        return usetToken;
    }
}
