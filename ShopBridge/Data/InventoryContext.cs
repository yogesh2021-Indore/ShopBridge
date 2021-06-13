using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;

namespace ShopBridge.Data
{
    public class InventoryContext : DbContext
    {
        public static string connectionString = string.Empty;
        public DbSet<Product> Product {get;set;}
        public InventoryContext(DbContextOptions<InventoryContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
