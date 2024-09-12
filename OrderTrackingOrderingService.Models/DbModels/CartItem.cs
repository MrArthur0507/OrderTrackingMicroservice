using OrderTrackingOrdereringService.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderTrackingOrdereringService.Models.DbModels
{
    public class CartItem : Entity
    {
        public Guid CartId { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }

        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }
    }
}
