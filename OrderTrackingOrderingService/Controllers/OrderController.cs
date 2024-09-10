using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingOrdereringService.Models.DbModels;
using OrderTrackingOrderingService.DataAccess.Context;

namespace OrderTrackingOrderingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderingDbContext _context;
        public OrderController(OrderingDbContext orderingDbContext)
        {
            _context = orderingDbContext;
        }


        [HttpGet]
        [Route("getUserOrders")]
        [Authorize]
        public IActionResult GetUserOrders()
        {
            var orders = _context.Orders.Where(x => x.CustomerName == User.Identity.Name);
            return Ok(orders);
        }

        [HttpPost]
        [Route("createOrder")]
        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            Console.WriteLine(User.Identity.Name);
            Order order = new Order();
            order.CustomerName = User.Identity.Name;
            order.OrderDate = DateTime.Now;
            order.ShippingAddress = "test";
            
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order.Id);
        }
    }
}
