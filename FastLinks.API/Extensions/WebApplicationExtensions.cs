using Microsoft.OpenApi.Models;

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