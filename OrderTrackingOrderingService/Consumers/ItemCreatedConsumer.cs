using AutoMapper;
using MassTransit;
using MassTransitContracts.ItemCatalogContracts;
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
        public Task Consume(ConsumeContext<ItemCreated> context)
        {
            return Task.CompletedTask;
        }
    }
}
