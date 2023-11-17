using FastLinks.Application;
using FastLinks.Persistence;
using FastLinks.Persistence.Identity;
using FastLinks.API.Endpoints;
using FastLinks.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.ConfigureSwaggerDoc();


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

app.UseHttpsRedirection();

// Adds /register, /login and /refresh endpoints
app.MapIdentityApi<ApplicationUser>();

app.MapEndpoints();


app.Run();

