using FastLinks.Application.Contracts.Auth;
using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse>
{
    public readonly IAuthenticationService _authenticationService;
    public DeleteUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService= authenticationService;
    }

    public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
