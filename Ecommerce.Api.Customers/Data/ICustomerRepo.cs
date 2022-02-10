using Ecommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Data
{
    public interface ICustomerRepo
    {
        //Task<List<CustomerModel>> GetAllCustomers();
        Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, CustomerModel Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
