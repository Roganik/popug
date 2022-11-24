using Microsoft.EntityFrameworkCore;
using popug.webinfra;
using sso.db;
using sso.webapi;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.ConfigurePopugServices();
services.AddDbContext<SsoDbContext>(options => options.UseSqlite(DesignTimeDbContextFactory.ConnectionString));

var app = builder.Build();
app.ConfigurePopugWebApplication();

app.MapGet("/", (SsoDbContext db) =>
{
    db.Database.Migrate(); //todo: dirty
    return Results.Redirect("/swagger");
});

app.MapGet("/users", UsersApiHandlers.GetUsers).AllowAnonymous();
app.MapPut("/users", UsersApiHandlers.CreateUser).RequireAuthorization();
app.MapPost("/users", UsersApiHandlers.UpdateUser).RequireAuthorization(opts => opts.RequireRole("Admin"));
app.MapPost("/login", UsersApiHandlers.Login).AllowAnonymous();
app.MapPost("/validateJwt", UsersApiHandlers.ValidateJwt).AllowAnonymous();

app.Run();