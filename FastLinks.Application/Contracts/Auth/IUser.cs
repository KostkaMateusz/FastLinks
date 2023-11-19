namespace FastLinks.Application.Contracts.Auth;
public interface IUser
{
    Guid UserId { get; }
    string? UserName { get; }
}