using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitContracts.OrderingContracts
{
    public class OrderCreated
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }

        public ICollection<OrderItemCreated> OrderItems { get; set; } = new List<OrderItemCreated>();
    }
}
