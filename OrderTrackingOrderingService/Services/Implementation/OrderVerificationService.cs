using MassTransit;
using MassTransitContracts.OrderingContracts;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.Services.Contracts;

namespace OrderTrackingOrderingService.Services.Implementation
{
    public class OrderVerificationService : IOrderVerificationService
    {
        private readonly IPublishEndpoint _publisher;

        public OrderVerificationService(IPublishEndpoint publishEndpoint)
        {
            _publisher = publishEndpoint;
        }

        public async Task SendOrderForVerification(Order order)
        {
            OrderCreated orderCreated = MapOrder(order);

            await _publisher.Publish(orderCreated);
        }


        private OrderCreated MapOrder(Order order)
        {
            OrderCreated orderCreated = new OrderCreated();
            orderCreated.OrderDate = order.OrderDate;
            orderCreated.ShippingAddress = order.ShippingAddress;
            orderCreated.CustomerName = order.CustomerName;
            orderCreated.OrderItems = MapOrderItems(order.OrderItems);
            orderCreated.Id = order.Id;

            return orderCreated;
        } 


        private List<OrderItemCreated> MapOrderItems(ICollection<OrderItem> orderItems)
        {
            List<OrderItemCreated> orderItemsCreated = new List<OrderItemCreated>();

            foreach (var orderItem in orderItems)
            {
                OrderItemCreated orderItemCreated = new OrderItemCreated();
                orderItemCreated.OrderId = orderItem.OrderId;
                orderItemCreated.Quantity = orderItem.Quantity;
                orderItemCreated.UnitPrice = orderItem.UnitPrice;
                orderItemCreated.ItemId = orderItem.ItemId;
                orderItemsCreated.Add(orderItemCreated);
            }

            return orderItemsCreated;
        }
    }
}
