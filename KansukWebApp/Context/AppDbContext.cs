using Microsoft.EntityFrameworkCore;

namespace KansukWebApp.Context
{
    public class AppDbContext : DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<KansukWebApp.Models.User> Users { get; set; }
        public DbSet<KansukWebApp.Models.Assignment> Assignments { get; set; }
    }
}
