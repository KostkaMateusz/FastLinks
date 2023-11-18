using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Commands.DeleteUrlLink;

public class DeleteLinkCommand : IRequest 
{
    public string ShortUrlAddress { get; set; }
    public  Guid UserId { get; set; }

    public DeleteLinkCommand(string ShortUrlAddress , Guid UserId)
    {
        this.ShortUrlAddress = ShortUrlAddress;
        this.UserId = UserId;
    }
}