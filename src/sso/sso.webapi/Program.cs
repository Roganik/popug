using Microsoft.EntityFrameworkCore;
using popug.webinfra;
using sso.db;
using sso.webapi;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.ConfigurePopugServices();
services.AddDbContext<SsoDbContext>(options => options.UseSqlite(DesignTimeDbContextFactory.ConnectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // migrate db on startup
    var db = scope.ServiceProvider.GetService<SsoDbContext>();
    db.Database.Migrate();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.ConfigurePopugWebApplication();

app.MapGet("/users", UsersApiHandlers.GetUsers).AllowAnonymous();
app.MapPut("/users", UsersApiHandlers.CreateUser).RequireAuthorization();
app.MapPost("/users", UsersApiHandlers.UpdateUser).RequireAuthorization(opts => opts.RequireRole("Admin"));
app.MapPost("/login", UsersApiHandlers.Login).AllowAnonymous();
app.MapPost("/validateJwt", UsersApiHandlers.ValidateJwt).AllowAnonymous();

app.Run();