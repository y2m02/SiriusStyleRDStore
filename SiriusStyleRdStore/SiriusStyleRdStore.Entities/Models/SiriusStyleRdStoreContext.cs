using Microsoft.EntityFrameworkCore;

namespace SiriusStyleRdStore.Entities.Models
{
    public class SiriusStyleRdStoreContext : DbContext
    {
        public SiriusStyleRdStoreContext()
        {
        }

        public SiriusStyleRdStoreContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        //public DbSet<OrderLine> OrderLine { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Size> Size { get; set; }
    }
}