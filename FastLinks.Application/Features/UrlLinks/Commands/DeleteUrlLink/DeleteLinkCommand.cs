using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Commands.DeleteUrlLink;

public class DeleteLinkCommand : IRequest 
{
    public required string ShortUrlAddress { get; set; }
    public required Guid UserId { get; set; }
}