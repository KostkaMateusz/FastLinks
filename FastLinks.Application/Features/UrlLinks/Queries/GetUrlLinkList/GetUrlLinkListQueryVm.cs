
namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkList;

public class GetUrlLinkListQueryVm
{
    public required string UrlAddress { get; set; }
    public required string ShortUrlAddress { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int NumberOfEntries { get; set; }
    public Guid? UserCreatorId { get; set; }
}