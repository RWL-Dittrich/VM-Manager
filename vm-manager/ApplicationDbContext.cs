using Microsoft.EntityFrameworkCore;
using vm_manager.Models;

namespace vm_manager;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Server> Servers { get; set; }
    public DbSet<ServerAuthentication> ServerAuthentications { get; set; }
    public DbSet<PrivateKey> PrivateKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Server>()
            .HasOne(s => s.ServerAuthentication)
            .WithOne(sa => sa.Server)
            .HasForeignKey<ServerAuthentication>(sa => sa.ServerId);
    }
}