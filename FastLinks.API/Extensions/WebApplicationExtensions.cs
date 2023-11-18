using FastLinks.API.Endpoints;
using FastLinks.API.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FastLinks.API.Extensions;

public static class WebApplicationExtensions
{
    public static IServiceCollection ConfigureSwaggerDoc(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "FastLinks", Version = "v1", Description = "This project is to create small backend service for  creating short links in ASP.NET" });

            options.AddSecurityDefinition("FastLinkApiBearerAuth", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid token to access this API"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "FastLinkApiBearerAuth" }
                    }, new List<string>()
                }
            });
        });

        return services;
    }

    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GetType().Name;

        return app
                    .MapGroup($"/api/{groupName}")
                    .WithTags(groupName)
                    .WithOpenApi();
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        //Type of Base Class
        var endpointGroupType = typeof(EndpointGroupBase);
        //Get Curent Assembly
        var assembly = Assembly.GetExecutingAssembly();
        // Get public types in assembly
        var types = assembly.GetExportedTypes();
        // Filter types to those that derive from EndpointGroupBase
        var endpointGroupTypes = types.Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (Type type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                instance.Map(app);
            }
        }
        return app;
    }

    public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
            {
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyOrigin();
            });
        });
        return builder;
    }


}