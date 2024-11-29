using Inance.Models;
using Microsoft.EntityFrameworkCore;

namespace Inance.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<Service> Services { get; set; }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(e => e.Service)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(e => e.Master)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.MasterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Master>()
            .HasOne(e => e.Service)
            .WithMany(e => e.Masters)
            .HasForeignKey(e => e.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
