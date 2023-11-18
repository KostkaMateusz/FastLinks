namespace FastLinks.Application.Contracts.Identity;
public interface IUser
{
    string? UserName { get; }
    Guid UserId { get; }
}