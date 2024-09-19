using Microsoft.EntityFrameworkCore;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.DataAccess.Context;
using OrderTrackingOrderingService.Models.DTOs;
using OrderTrackingOrderingService.Services.Contracts;

namespace OrderTrackingOrderingService.Services.Implementation
{
    public class OrderService : IOrderService
    {

        private readonly OrderingDbContext _dbContext;
        private readonly IOrderVerificationService _orderVerificationService;
        public OrderService(OrderingDbContext orderingDbContext, IOrderVerificationService orderVerificationService = null)
        {
            _dbContext = orderingDbContext;
            _orderVerificationService = orderVerificationService;
        }

        public async Task<List<OrderDto>> GetOrders(string username)
        {
            List<Order> orders = await _dbContext.Orders.Where(o => o.CustomerName == username).Include(o => o.OrderItems).ToListAsync();

            List<OrderDto> orderDtos = new List<OrderDto>();

            
            foreach (var order in orders)
            {
                OrderDto orderDto = new OrderDto();
                orderDto.TotalAmount = order.TotalAmount;
                orderDto.CustomerName = order.CustomerName;
                orderDto.OrderItems = convertToDto(order.OrderItems.ToList());
                orderDto.OrderDate = order.OrderDate;
                orderDto.ShippingAddress = order.ShippingAddress;
                orderDtos.Add(orderDto);
            }
            return orderDtos;
        }


        private List<OrderItemDto> convertToDto(List<OrderItem> items)
        {
            List<OrderItemDto> orderItemDtos = new List<OrderItemDto>();

            foreach (var item in items)
            {
                OrderItemDto orderItemDto = new OrderItemDto();

                orderItemDto.ItemId = item.ItemId;
                orderItemDto.Quantity = item.Quantity;
                orderItemDtos.Add(orderItemDto);
            }
            return orderItemDtos;
        }
        
        public async Task<bool> SubmitOrder(string username)
        {

            Cart cart = await _dbContext.Carts
                .Include(c => c.Items)                     
                .ThenInclude(ci => ci.Item)       
                .FirstOrDefaultAsync(c => c.CustomerUsername == username);

            if (cart == null)
            {
                return false; 
            }

            Order order = new Order();
            order.CustomerName = username;
            order.ShippingAddress = "testOnly";  
            order.OrderItems = new List<OrderItem>();

            decimal totalPrice = cart.Items.Sum(ci => ci.Item.Price * ci.Quantity);  
            order.TotalAmount = totalPrice;

            foreach (var cartItem in cart.Items)
            {
                OrderItem orderItem = new OrderItem
                {
                    ItemId = cartItem.ItemId,  
                    Quantity = cartItem.Quantity, 
                    UnitPrice = cartItem.Item.Price 
                };

                order.OrderItems.Add(orderItem);  
            }


            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            await _orderVerificationService.SendOrderForVerification(order);
            return true;

        }
    }
}
