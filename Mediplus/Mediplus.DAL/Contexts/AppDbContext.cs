using Mediplus.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mediplus.DAL.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<SliderItem> SliderItems { get; set; }

    public DbSet<Portfolio> Portfolios { get; set; }

    public DbSet<Partner> Partners { get; set; }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Setting> Settings { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<HospitalsDoctors> HospitalsDoctors { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Hospital> Hospitals { get; set; }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<HospitalsDoctors>()
            .HasKey(e => new { e.HospitalId, e.DoctorId });

        builder.Entity<HospitalsDoctors>()
            .HasOne(e => e.Hospital)
            .WithMany(h => h.HospitalsDoctors)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<HospitalsDoctors>()
            .HasOne(e => e.Doctor)
            .WithMany(d => d.HospitalsDoctors)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
    }
}
