using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace sso.db;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SsoDbContext>
{
    public static string ConnectionString = "Data Source=/Users/roganik/Projects/popug/src/sso-db.sqlite";
    
    public SsoDbContext CreateDbContext(string[] args)
    {
        var dbContextOptions = new DbContextOptionsBuilder<SsoDbContext>()
            .UseSqlite(ConnectionString).Options;

        return new SsoDbContext(dbContextOptions);
    }
}