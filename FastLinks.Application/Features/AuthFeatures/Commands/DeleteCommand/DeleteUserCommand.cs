using MediatR;

namespace FastLinks.Application.Features.AuthFeatures.Commands.DeleteCommand;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }

    public DeleteUserCommand(Guid UserId)
    {
        this.UserId = UserId;
    }
}
