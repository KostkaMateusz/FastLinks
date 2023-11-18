namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetail;

public class GetUrlLinkDetailsQueryVm
{
    public required string UrlAddress { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int NumberOfEntries { get; set; }
    public Guid? UserCreatorId { get; set; }
}
