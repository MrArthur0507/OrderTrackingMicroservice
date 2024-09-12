using Microsoft.EntityFrameworkCore;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrdereringService.Models.DTOs;
using OrderTrackingOrderingService.DataAccess.Context;
using OrderTrackingOrderingService.Models.DTOs;
using OrderTrackingOrderingService.Services.Contracts;

namespace OrderTrackingOrderingService.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly OrderingDbContext _dbContext;

        public CartService(OrderingDbContext orderingDbContext)
        {
            _dbContext = orderingDbContext;
        }


        public async Task<CartDto> GetCartWithItems(string username)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Item)  
                .FirstOrDefaultAsync(c => c.CustomerUsername == username);

            if (cart == null)
            {
                return null; 
            }

            CartDto cartDto = new CartDto();
            cartDto.CustomerUsername = username;
            List<CartItemDto> items = new List<CartItemDto>();
            foreach (var item in cart.Items)
            {
                CartItemDto cartItemDto = new CartItemDto();
                cartItemDto.ItemId = item.Id;
                cartItemDto.Quantity = item.Quantity;
                items.Add(cartItemDto);
            }
            cartDto.Items = items;

            return cartDto;
        }




        private async Task<Cart> GetOrCreateCart(string username)
        {
            var existingCart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.CustomerUsername == username);
            if (existingCart != null)
            {
                return existingCart;
            }

            Cart cart = new Cart { CustomerUsername = username };
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> CreateCart(string username)
        {
            return await GetOrCreateCart(username);
        }

        public async Task ClearCart(string username)
        {
            var existingCart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.CustomerUsername == username);
            if (existingCart != null)
            {
                List<CartItem> cartItems = await _dbContext.CartItems
                    .Where(ci => ci.CartId == existingCart.Id)
                    .ToListAsync();

                _dbContext.CartItems.RemoveRange(cartItems);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AddItemToCart(string username, CartItemAddDto cartItemAddDto)
        {
            var existingCart = await GetOrCreateCart(username);

            var itemExist = await _dbContext.Items.FindAsync(cartItemAddDto.ItemId);
            if (itemExist == null)
            {
                return false;
            }

            CartItem cartItem = new CartItem
            {
                ItemId = itemExist.Id,
                Quantity = cartItemAddDto.Quantity
            };
            if (existingCart.Items == null)
            {
                existingCart.Items = new List<CartItem>();
            }
            existingCart.Items.Add(cartItem);
            await _dbContext.SaveChangesAsync(); 

            return true;
        }

        public async Task<bool> RemoveItemFromCart(string username, Guid itemId)
        {
            var existingCart = await _dbContext.Carts
                .Include(c => c.Items) 
                .FirstOrDefaultAsync(c => c.CustomerUsername == username);

            if (existingCart == null)
            {
                return true; 
            }

            var itemToRemove = existingCart.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (itemToRemove != null)
            {
                existingCart.Items.Remove(itemToRemove);
                await _dbContext.SaveChangesAsync(); 
            }

            return true;
        }
    }
}
