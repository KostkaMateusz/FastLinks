using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FastLinks.Application.Contracts.Identity;
using FastLinks.Application.Contracts.Persistence;
using FastLinks.Persistence.Repositories;
using FastLinks.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace FastLinks.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("FastLinkDbConnectionString"));

        services.AddDbContext<FastLinksDbContext>(options =>
        {
            options.UseSqlServer(dbConnectionString);
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IUrlLinkRepository, UrlLinkRepository>();

        services.AddIdentityConfiguration(configuration);

        return services;    
    }
}
