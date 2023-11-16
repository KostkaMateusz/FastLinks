using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Commands.UpdateUrlLink;

public class UpdateLinkCommand : IRequest<string> 
{
    public required string ShortUrlAddress { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid UserId { get; set; }
}