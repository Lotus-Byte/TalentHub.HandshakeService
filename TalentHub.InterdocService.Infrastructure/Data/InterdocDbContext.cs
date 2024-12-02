using Microsoft.EntityFrameworkCore;
using TalentHub.InterdocService.Infrastructure.Models;

namespace TalentHub.InterdocService.Infrastructure.Data;

public class InterdocDbContext : DbContext
{
    public InterdocDbContext(){}
    public InterdocDbContext(DbContextOptions<InterdocDbContext> options) : base(options) { }
    
    public DbSet<Interdoc> Interdocs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблицы Interdoc
        modelBuilder.Entity<Interdoc>()
            .ToTable("Interdocs")
            .HasKey(i => i.InterdocId);

        base.OnModelCreating(modelBuilder);
    }
}