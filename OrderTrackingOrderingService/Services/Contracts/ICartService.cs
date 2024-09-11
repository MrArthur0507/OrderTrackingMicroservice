using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrdereringService.Models.DTOs;

namespace OrderTrackingOrderingService.Services.Contracts
{
    public interface ICartService
    {
        Task<Cart> CreateCart(string username); 

        Task ClearCart(string username); 

        Task<bool> AddItemToCart(string username, CartItemAddDto cartItemAddDto); 

        Task<bool> RemoveItemFromCart(string username, Guid itemId); 
    }
}
