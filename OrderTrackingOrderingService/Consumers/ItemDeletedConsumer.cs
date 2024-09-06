using MassTransit;
using MassTransitContracts.ItemCatalogContracts;
using Microsoft.EntityFrameworkCore;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.DataAccess.Context;

namespace OrderTrackingOrderingService.Consumers
{
    public class ItemDeletedConsumer : IConsumer<ItemDeleted>
    {
        private readonly OrderingDbContext _context;

        public ItemDeletedConsumer(OrderingDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ItemDeleted> context)
        {
            var message = context.Message;

            Item item = await _context.Items.FindAsync(message.Id);

            if (item == null)
            {
                Console.WriteLine($"Item with Id {message.Id} not found.");
                return;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
