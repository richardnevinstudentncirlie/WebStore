using WebStore.Domain.Entities;
using System.Data.Entity;

namespace WebStore.Domain.Concrete {

    public class EFDbContext : DbContext {
        public DbSet<Product> Products { get; set; }
    }
}