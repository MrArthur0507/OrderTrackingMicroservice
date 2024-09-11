using OrderTrackingOrdereringService.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrdereringService.Models.DbModels
{
    public class CartItem : Entity
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }
    }
}
