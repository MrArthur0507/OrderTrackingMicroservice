using OrderTrackingOrdereringService.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrderingService.Models.DTOs
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
