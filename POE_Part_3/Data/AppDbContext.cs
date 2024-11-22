using Microsoft.EntityFrameworkCore;
using POE_Part_3.Models;

namespace POE_Part_3.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base (options)
        {
        }

        // DbSet properties map your models to database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
    }
}

