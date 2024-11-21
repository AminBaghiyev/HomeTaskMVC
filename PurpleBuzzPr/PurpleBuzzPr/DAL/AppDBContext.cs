using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.DAL
{
    public class AppDBContext : DbContext
    {
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        public AppDBContext(DbContextOptions options) : base(options) { }
    }
}
