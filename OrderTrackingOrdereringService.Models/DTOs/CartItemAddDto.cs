using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrdereringService.Models.DTOs
{
    public class CartItemAddDto
    {
        public Guid ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
