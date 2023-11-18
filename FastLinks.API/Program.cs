using FastLinks.Application;
using FastLinks.Persistence;
using FastLinks.Persistence.Identity;
using FastLinks.API.Services;
using FastLinks.Application.Contracts.Identity;
using FastLinks.API.Extensions;
using FastLinks.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.ConfigureSwaggerDoc();

builder.Services.AddScoped<IUser, CurrentUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
    });
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

// Adds /register, /login and /refresh endpoints
app.MapIdentityApi<ApplicationUser>();

app.MapEndpoints();


app.Run();

