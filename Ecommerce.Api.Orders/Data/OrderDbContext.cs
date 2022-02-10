using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<OrderItemEntity> OrderItem { get; set; }
    }
}
