using OrderTrackingOrdereringService.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrdereringService.Models.DbModels
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }  

        
        public Guid OrderId { get; set; }
        public Order Order { get; set; }  

        public Guid ItemId { get; set; }
        public Item Item { get; set; }  
    }

}
