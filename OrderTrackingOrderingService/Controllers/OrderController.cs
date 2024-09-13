using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.DataAccess.Context;
using OrderTrackingOrderingService.Models.DTOs;
using OrderTrackingOrderingService.Services.Contracts;

namespace OrderTrackingOrderingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Route("submitOrder")]
        public async Task<IActionResult> SubmitOrder()
        {
            bool result = await _orderService.SubmitOrder(User.Identity.Name);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getOrders")]
        public async Task<IActionResult> GetOrders()
        {
            List<OrderDto> orders = await _orderService.GetOrders(User.Identity.Name);

            return Ok(orders);
        }

    }
}
