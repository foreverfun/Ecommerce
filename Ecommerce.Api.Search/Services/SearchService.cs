using Ecommerce.Api.Search.Services.CustomerService;
using Ecommerce.Api.Search.Services.OrderService;
using Ecommerce.Api.Search.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customersService = customersService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);
            var productsResult = await _productsService.GetProductsAsync();
            var customersResult = await _customersService.GetCustomersAsync();

            if (ordersResult.IsSuccess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    order.Customer = new ();
                    order.Customer.Id = customersResult.Customers.FirstOrDefault(c => c.Id == customerId).Id;
                    order.Customer.LastName = customersResult.IsSuccess ?
                        customersResult.Customers.FirstOrDefault(c => c.Id == customerId)?.LastName :
                        "Customer information is not available";
                    order.Customer.FirstName = customersResult.IsSuccess ?
                        customersResult.Customers.FirstOrDefault(c => c.Id == customerId)?.FirstName :
                        "Customer information is not available";

                    foreach (var item in order.OrderItems)
                    {
                        item.ProductName = productsResult.IsSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == Int32.Parse(item.ProductId))?.Name :
                            "Product information is not available";
                    }
                }

                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
