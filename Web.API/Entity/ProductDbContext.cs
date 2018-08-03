using Microsoft.EntityFrameworkCore;

namespace Web.API.Entity
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
