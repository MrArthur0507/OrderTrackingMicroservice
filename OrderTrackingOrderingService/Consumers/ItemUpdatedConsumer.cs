using MassTransit;
using MassTransitContracts.ItemCatalogContracts;
using Microsoft.EntityFrameworkCore;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.DataAccess.Context;
using SQLitePCL;

namespace OrderTrackingOrderingService.Consumers
{
    public class ItemUpdatedConsumer : IConsumer<ItemUpdated>
    {
        private readonly OrderingDbContext _context;
        public ItemUpdatedConsumer(OrderingDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ItemUpdated> context)
        {
            var message = context.Message;

            Item item = await _context.Items.FindAsync(message.Id);
            if (item == null)
            {
                Console.WriteLine($"Item with Id {message.Id} not found.");
                return;
            }

            item.Name = message.Name;
            item.Description = message.Description;
            item.Price = message.Price;
            item.StockQuantity = message.StockQuantity;

            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
