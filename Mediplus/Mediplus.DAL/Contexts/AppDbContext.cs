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

    public AppDbContext(DbContextOptions options) : base(options) { }
}
