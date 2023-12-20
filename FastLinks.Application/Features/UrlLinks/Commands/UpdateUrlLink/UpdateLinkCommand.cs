using MediatR;

namespace FastLinks.Application.Features.UrlLinks.Commands.UpdateUrlLink;

public class UpdateLinkCommand : IRequest<string>
{
    public string ShortUrlAddress { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid UserId { get; set; }

    public UpdateLinkCommand(string ShortUrlAddress, DateTime ExpirationDate, Guid UserId)
    {
        this.ShortUrlAddress = ShortUrlAddress;
        this.ExpirationDate = ExpirationDate;
        this.UserId = UserId;
    }
}