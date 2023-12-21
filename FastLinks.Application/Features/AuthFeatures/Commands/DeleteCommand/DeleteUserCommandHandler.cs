using FastLinks.Application.Contracts.Auth;
using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    public readonly IAuthenticationService _authenticationService;
    public DeleteUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService= authenticationService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _authenticationService.DeleteUserAsync(request);

        return true;
    }
}
