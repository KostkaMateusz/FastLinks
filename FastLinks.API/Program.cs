using FastLinks.Application;
using FastLinks.Persistence;
using FastLinks.Identity;
using FastLinks.API.Services;
using FastLinks.API.Extensions;
using FastLinks.API.Endpoints;
using YourProjectName.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddScoped<IUser, CurrentUser>();

builder.Services.ConfigureSwaggerDoc();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseExceptionHandler(options => { });

app.UseAuthorization();

app.AddUrlLinksEndpoint();
app.AddUrlLinkRedirection();
app.AddAccountController();

app.Run();