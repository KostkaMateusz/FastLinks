using FastLinks.Application.Contracts.Auth;
using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;

public class RegistrationRequestCommandHandler : IRequestHandler<RegistrationRequestCommand, RegistrationRequestCommandResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public RegistrationRequestCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<RegistrationRequestCommandResponse> Handle(RegistrationRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new RegistrationRequestCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var newUser = await _authenticationService.RegisterAsync(request);

        return newUser;
    }
}
