using AutoMapper;
using Ecommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Data
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderRepo(OrderDbContext context, IMapper mapper, ILogger<OrderRepo> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Order.Any())
            {
                var orders = new List<OrderEntity>()
                {
                    new () {
                        Id = 1,
                        CustomerId = 1,
                        OrderDate = DateTime.Now,
                        OrderItems = new ()
                        {
                            new () { OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 10},
                            new () { OrderId = 1, ProductId = 2, Quantity = 2, UnitPrice = 20 },
                            new () { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 30},
                            new () { OrderId = 2, ProductId = 2, Quantity = 5, UnitPrice = 20 },
                            new () { OrderId = 2, ProductId = 3, Quantity = 8, UnitPrice = 30 }
                        },
                        Total = 100
                    },
                    new () {
                        Id = 2,
                        CustomerId = 2,
                        OrderDate = DateTime.Now.AddDays(-1),
                        OrderItems = new ()
                        {
                            new () { OrderId = 2, ProductId = 2, Quantity = 5, UnitPrice = 20 },
                            new () { OrderId = 2, ProductId = 3, Quantity = 8, UnitPrice = 30 }
                        }
                    },
                    new () {
                        Id = 3,
                        CustomerId = 2,
                        OrderDate = DateTime.Now.AddDays(-3),
                        OrderItems = new ()
                        {
                            new () { OrderId = 1, ProductId = 2, Quantity = 5, UnitPrice = 40 },
                            new () { OrderId = 1, ProductId = 3, Quantity = 8, UnitPrice = 50 }
                        },
                        Total = 100
                    },
                    new () {
                        Id = 4,
                        CustomerId = 3,
                        OrderDate = new DateTime(2021, 12, 15),
                        OrderItems = new ()
                        {
                            new () { OrderId = 1, ProductId = 1, Quantity = 5, UnitPrice = 40 },
                            new () { OrderId = 2, ProductId = 2, Quantity = 8, UnitPrice = 50 }
                        },
                        Total = 100
                    }
                };
                _context.AddRange(orders);
                _context.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, List<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                _logger?.LogInformation("Querying Orders");
                var orders = await _context.Order
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.OrderItems)
                    .ToListAsync();
                if (orders != null && orders.Any())
                {
                    _logger?.LogInformation($"{orders.Count} Orders found");
                    var result = _mapper.Map<List<OrderModel>>(orders);
                    return (true, result, null);
                }
                return (false, null, "No order is found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        //public async Task<(bool IsSuccess, OrderModel Order, string ErrorMessage)> GetOrderAsync(int id)
        //{
        //    try
        //    {
        //        _logger?.LogInformation($"Querying Orders with id {id}");
        //        var order = await _context.Order.FirstOrDefaultAsync(o => o.Id == id);
        //        if (order != null)
        //        {
        //            _logger.LogInformation($"Order {id} is found.");
        //            var result =_mapper.Map<OrderModel>(order);
        //            return (true, result, null);
        //        }
        //        return (false, null, $"Order {id} is not found.")
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger?.LogError(ex.ToString());
        //        return (false, null, ex.Message);
        //    }
        //}

        //public async Task<List<OrderModel>> GetOrders(int userId)
        //{
        //    return await Task.FromResult(
        //        _mapper.Map<List<OrderEntity>, List<OrderModel>>(
        //            _context.Order.Where(o => o.UserId == userId).ToList()));
        //}

        //public async Task<List<OrderItemModel>> GetOrderItems(int orderId)
        //{            
        //    return await Task.FromResult(
        //        _mapper.Map<List<OrderItemEntity>, List<OrderItemModel>>(
        //            _context.OrderItem.Where(oi => oi.OrderId == orderId).ToList()));
        //}

       
    }
}
