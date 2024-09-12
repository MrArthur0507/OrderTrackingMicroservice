using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrderingService.Models.DTOs
{
    public class CartItemDto
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
