using Microsoft.EntityFrameworkCore;
using sso.db.Models;

namespace sso.db;

public class SsoDbContext : DbContext
{
    public SsoDbContext(DbContextOptions opts) : base(opts)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(f => f.Login)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}