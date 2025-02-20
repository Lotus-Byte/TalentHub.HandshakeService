using Microsoft.EntityFrameworkCore;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Data;

public class HandshakeDbContext : DbContext
{
    public HandshakeDbContext(){}
    public HandshakeDbContext(DbContextOptions<HandshakeDbContext> options) : base(options) { }
    
    public DbSet<Handshake> Handshakes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблицы Applications
        modelBuilder.Entity<Handshake>()
            .ToTable("Applications")
            .HasKey(i => i.HandshakeId);

        base.OnModelCreating(modelBuilder);
    }
}