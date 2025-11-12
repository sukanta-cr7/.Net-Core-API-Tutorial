using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_Tutorial.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
    }
}
