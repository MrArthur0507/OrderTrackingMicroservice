using Microsoft.EntityFrameworkCore;
using OrderTrackingItemCatalogService.Models.ItemCatalogModels;

namespace OrderTrackingItemCatalogService.Data
{
    public class ItemCatalogContext : DbContext
    {
        public ItemCatalogContext(DbContextOptions<ItemCatalogContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
