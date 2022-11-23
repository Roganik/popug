using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Popug.SharedLibs;
using Popug.SharedLibs.Impl;
using sso.bl.Commands;
using sso.db;
using sso.webapi;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.RegisterLibs();
services.AddDbContext<SsoDbContext>(options => options.UseSqlite(DesignTimeDbContextFactory.ConnectionString));
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Popug SSO API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", (SsoDbContext db) =>
{
    db.Database.Migrate(); //todo: dirty
    return Results.Redirect("/swagger");
});

app.MapGet("/users", UsersApiHandlers.GetUsers);
app.MapPut("/users", UsersApiHandlers.CreateUser);
app.MapPost("/users", UsersApiHandlers.UpdateUser);
app.MapPost("/login", UsersApiHandlers.Login);

app.Run();