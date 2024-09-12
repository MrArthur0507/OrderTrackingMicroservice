using OrderTrackingOrdereringService.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrdereringService.Models.DbModels
{
    public class Cart : Entity
    {
        public string CustomerUsername { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
