using WebStore.Domain.Entities;
using System.Data.Entity;

namespace WebStore.Domain.Concrete 
{

    public class EFDbContext : DbContext 
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }

}