using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.Models.DTOs;

namespace OrderTrackingOrderingService.Services.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrders(string username);
        Task<bool> SubmitOrder(string username);
    }
}
