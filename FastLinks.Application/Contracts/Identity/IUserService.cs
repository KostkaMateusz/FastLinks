

using FastLinks.Application.Responses;

namespace FastLinks.Application.Contracts.Identity;

public interface IUserService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(BaseResponse Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<BaseResponse> DeleteUserAsync(string userId);
}
