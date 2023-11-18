namespace FastLinks.API.Services;
public interface IUser
{
    Guid UserId { get; }
    string? UserName { get; }
}