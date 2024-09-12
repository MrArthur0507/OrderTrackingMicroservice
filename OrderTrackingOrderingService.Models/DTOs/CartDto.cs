using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrderingService.Models.DTOs
{
    public class CartDto
    {
        public string CustomerUsername { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
