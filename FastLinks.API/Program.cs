using FastLinks.Application;
using FastLinks.Persistence;
using FastLinks.Identity;
using FastLinks.API.Services;
using FastLinks.API.Extensions;
using FastLinks.Api.Middleware;
using FastLinks.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddScoped<IUser, CurrentUser>();

builder.Services.ConfigureSwaggerDoc();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCustomExceptionHandler();

app.MapEndpoints();

app.UseAuthorization();

app.AddUrlLinksEndpoint();

app.Run();