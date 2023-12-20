using FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;
using FastLinks.Application.Features.AuthFeatures.Commands.RegisterCommand;
using FastLinks.Application.Features.AuthFeatures.Queries.AuthenticationTokenQuery;

namespace FastLinks.Application.Contracts.Auth;

public interface IAuthenticationService
{
    Task<AuthenticationTokenQueryResponse> AuthenticateAsync(AuthenticationTokenQuery request);
    Task<RegistrationRequestCommandResponse> RegisterAsync(RegistrationRequestCommand request);
    Task<DeleteUserCommandResponse> DeleteUserAsync(DeleteUserCommand request)
}