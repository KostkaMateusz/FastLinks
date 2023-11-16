
namespace FastLinks.Application.Features.UrlLinks.Queries.GetUrlLinkDetails;

public class UrlLinkAddressVm
{
    public required string UrlAddress { get; set; }
    public DateTime ExpirationDate { get; set; } 
    public int NumberOfEntries { get; set; }
    public Guid? UserCreatorId { get; set; }
}
