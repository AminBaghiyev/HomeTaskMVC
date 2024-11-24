using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.DAL
{
    public class AppDBContext : DbContext
    {
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<WorkCategory> WorkCategories { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Work> Works { get; set; }

        public AppDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Service>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Work>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Works)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Work>()
                .HasOne(e => e.Service)
                .WithMany(e => e.Works)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
