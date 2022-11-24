using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Popug.SharedLibs;
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
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    }); 
});

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);
services.ConfigureOptions<ConfigureJwtBearerOptions>();
services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapGet("/", (SsoDbContext db) =>
{
    db.Database.Migrate(); //todo: dirty
    return Results.Redirect("/swagger");
});

app.MapGet("/users", UsersApiHandlers.GetUsers);
app.MapPut("/users", UsersApiHandlers.CreateUser).RequireAuthorization();
app.MapPost("/users", UsersApiHandlers.UpdateUser).RequireAuthorization();
app.MapPost("/login", UsersApiHandlers.Login);
app.MapPost("/validateJwt", UsersApiHandlers.ValidateJwt);

app.Run();