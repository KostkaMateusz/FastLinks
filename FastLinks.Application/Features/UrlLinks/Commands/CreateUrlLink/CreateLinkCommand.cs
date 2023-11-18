using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Commands.CreateUrlLink;

public class CreateLinkCommand : IRequest<CreateLinkCommandResponse>
{    
    public required string UrlAddress { get; set; }
    public Guid? UserCreatorId { get; set;}
}