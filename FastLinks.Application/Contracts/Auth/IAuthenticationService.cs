using FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;

namespace FastLinks.Application.Contracts.Auth;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    Task<RegistrationRequestCommandResponse> RegisterAsync(RegistrationRequestCommand request);
}