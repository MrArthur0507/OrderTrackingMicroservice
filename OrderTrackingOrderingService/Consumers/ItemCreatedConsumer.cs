using AutoMapper;
using MassTransit;
using MassTransitContracts.ItemCatalogContracts;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.DataAccess.Context;

namespace OrderTrackingOrderingService.Consumers
{
    public class ItemCreatedConsumer : IConsumer<ItemCreated>
    {
        private readonly OrderingDbContext _context;
        private readonly IMapper _mapper;
        public ItemCreatedConsumer(OrderingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<ItemCreated> context)
        {
            Item item = _mapper.Map<Item>(context.Message);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            await Console.Out.WriteLineAsync("Item added");
        }
    }
}
