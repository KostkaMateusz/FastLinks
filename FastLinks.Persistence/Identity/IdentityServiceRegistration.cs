using FastLinks.Application.Contracts.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastLinks.Persistence.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services
            .AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<FastLinksDbContext>()
            .AddApiEndpoints();

        services
            .AddSingleton(TimeProvider.System);        
        services
            .AddTransient<IUserService, UserService>();

        services.AddAuthorization();

        return services;
    }
}
