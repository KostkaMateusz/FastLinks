using FastLinks.Domain.Common;

namespace FastLinks.Domain.Entities;
public class UrlLink : AuditableEntity
{
    public required string UrlAddress { get; set; }
    public required string ShortUrlAddress { get; set; }
    public DateTime ExpirationDate { get; set; } = DateTime.Now.AddDays(7);
    public int NumberOfEntries { get; set; }
    public Guid? UserCreatorId { get; set; }
}