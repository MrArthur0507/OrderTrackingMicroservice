using OrderTrackingOrdereringService.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrdereringService.Models.DbModels
{
    public class Order : Entity
    {
        public Guid ItemId { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}
