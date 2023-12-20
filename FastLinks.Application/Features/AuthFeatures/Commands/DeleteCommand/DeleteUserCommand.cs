using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;

public class DeleteUserCommand : IRequest<DeleteUserCommandResponse>
{
    public Guid UserId { get; set; }
}
