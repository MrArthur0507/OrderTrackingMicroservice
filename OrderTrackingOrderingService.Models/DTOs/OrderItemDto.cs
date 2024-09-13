using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrderingService.Models.DTOs
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Guid OrderId { get; set; }

        public Guid ItemId { get; set; }

    }
}
