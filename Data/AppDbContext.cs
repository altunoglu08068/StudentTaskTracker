using Microsoft.EntityFrameworkCore;
using StudentTaskTracker.Models;

namespace StudentTaskTracker.Data;

public class AppDbContext : DbContext
{
    // Veritabanı tablolarımız (DbSet)
    public DbSet<TeamMember> Members { get; set; } = null!;
    public DbSet<ProjectTask> Tasks { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // İlişkisel model kuralları (Fluent API)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1-e-Çok İlişki: Bir görevin tek bir üyesi olabilir, üye silinirse görevi silme (SetNull)
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.AssignedMember)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
    }
}