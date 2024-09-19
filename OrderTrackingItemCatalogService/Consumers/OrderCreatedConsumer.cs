using MassTransit;
using MassTransitContracts.OrderingContracts;

namespace OrderTrackingItemCatalogService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            //Will implement the validation logic later. Now simply print the order with the items in it
            OrderCreated oc = context.Message;
            Console.WriteLine(oc.Id);
            foreach (var item in oc.OrderItems)
            {
                Console.WriteLine(item.ItemId);
                Console.WriteLine(item.Quantity);
            }

            return Task.CompletedTask;
        }
    }
}
