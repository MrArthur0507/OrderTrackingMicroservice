using Microsoft.EntityFrameworkCore;

namespace OrderTrackingItemCatalogService.Data
{
    public class ItemCatalogContext : DbContext
    {
        public ItemCatalogContext(DbContextOptions<ItemCatalogContext> options) : base(options) { }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
