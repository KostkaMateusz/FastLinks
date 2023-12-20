namespace FastLinks.Identity.Entities;

public sealed class ApplicationUser
{
    public Guid UserId { get; set; }
    public required string Email { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}