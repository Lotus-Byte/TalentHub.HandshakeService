using Microsoft.EntityFrameworkCore;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Data;

public class HandshakeDbContext : DbContext
{
    public HandshakeDbContext(){}
    public HandshakeDbContext(DbContextOptions<HandshakeDbContext> options) : base(options) { }
    
    public DbSet<Application> Applications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблицы Applications
        modelBuilder.Entity<Application>()
            .ToTable("Applications")
            .HasKey(i => i.ApplicationId);

        base.OnModelCreating(modelBuilder);
    }
}