using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Product_Catalog_New.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } 
    }
}