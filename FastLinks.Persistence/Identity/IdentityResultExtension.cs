

using FastLinks.Application.Responses;
using Microsoft.AspNetCore.Identity;

namespace FastLinks.Persistence.Identity;

public static class IdentityResultExtension
{
    public static BaseResponse ToApplicationResult(this IdentityResult result)
    {
        BaseResponse response;
        if (result.Succeeded)
        {
            response = new();
        }
        else
        {
            response = new(false)
            {
                ValidationErrors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        return response;
    }
}
