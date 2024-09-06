using Microsoft.EntityFrameworkCore;
using OrderTrackingOrdereringService.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingOrderingService.DataAccess.Context
{
    public class OrderingDbContext : DbContext
    {
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Item> Items { get; set; }

    }
}
