using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FastLinks.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg=>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());            
        });

        return services;
    }
}
