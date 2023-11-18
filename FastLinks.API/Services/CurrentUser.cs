using System.Security.Claims;

namespace FastLinks.API.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}