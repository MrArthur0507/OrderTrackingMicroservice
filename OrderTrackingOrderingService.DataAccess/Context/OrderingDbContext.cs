﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }


    }
}
