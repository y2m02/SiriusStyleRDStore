using Microsoft.EntityFrameworkCore;

namespace SiriusStyleRd.Entities.Models
{
    public class SiriusStyleRdContext : DbContext
    {
        public SiriusStyleRdContext()
        {
        }

        public SiriusStyleRdContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Bale> Bale { get; set; }
    }
}