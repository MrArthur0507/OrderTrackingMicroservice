using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingOrdereringService.Models.DTOs;
using OrderTrackingOrderingService.Services.Contracts;
using System.Security.Claims;

namespace OrderTrackingOrderingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }



        [HttpPost]
        [Route("clearCart")]
        public async Task<IActionResult> ClearCart()
        {
            var username = GetUsername();
            await _cartService.ClearCart(username);
            return NoContent();
        }


        [HttpPost]
        [Route("addItemToCart")]
        public async Task<IActionResult> AddItemToCart([FromBody] CartItemAddDto cartItemAddDto)
        {
            var username = GetUsername();

            var success = await _cartService.AddItemToCart(username, cartItemAddDto);
            if (success)
            {
                return Ok("Item added to cart successfully.");
            }

            return BadRequest("Failed to add item to cart.");
        }

        [HttpPost]
        [Route("removeItemFromCart")]
        public async Task<IActionResult> RemoveItemFromCart(Guid itemId)
        {
            var username = GetUsername();

            var success = await _cartService.RemoveItemFromCart(username, itemId);
            if (success)
            {
                return Ok("Item removed from cart successfully.");
            }

            return BadRequest("Failed to remove item from cart.");
        }


        private string GetUsername()
        {
            return User.Identity?.Name;
        }
    }
}
