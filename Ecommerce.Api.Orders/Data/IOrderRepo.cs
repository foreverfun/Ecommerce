using Ecommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Data
{
    public interface IOrderRepo
    {
        //Task<List<OrderModel>> GetOrders(int userId);
        //Task<List<OrderItemModel>> GetOrderItems(int orderId);
        Task<(bool IsSuccess, List<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
        //Task<(bool IsSuccess, OrderModel Order, string ErrorMessage)> GetOrderAsync(int id);
    }
}
