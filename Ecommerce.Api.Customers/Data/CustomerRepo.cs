using AutoMapper;
using Ecommerce.Api.Customers.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers.Data
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerRepo> _logger;

        public CustomerRepo(CustomerDbContext context, IMapper mapper, ILogger<CustomerRepo> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Customer.Any())
            {
                List<CustomerEntity> customers = new()
                {
                    new() { Id = 1, FirstName = "Mary", LastName = "Black", Address = "123 Mary Way"},
                    new() { Id = 2, FirstName = "John", LastName = "White", Address = "123 John Street" },
                    new() { Id = 3, FirstName = "CJ", LastName = "Lin", Address = "123 CJ Ave" }
                };
                _context.Customer.AddRange(customers);
                _context.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, CustomerModel Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                _logger?.LogInformation($"Get customers with id: {id}");
                var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    _logger?.LogInformation($"Customer {id} found");
                    var result = _mapper.Map<CustomerEntity, CustomerModel>(customer);
                    return (true, result, null);
                }
                return (false, null, $"Customer {id} not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);                
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                _logger?.LogInformation("Querying Customers");
                var customers = await _context.Customer.ToListAsync();
                if (customers != null)
                {
                    _logger?.LogInformation($"{customers.Count} Customers found");
                    var result = _mapper.Map<List<CustomerModel>>(customers);
                    return (true, result, null);
                }
                return (false, null, "No customer is found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        //public async Task<List<CustomerModel>> GetAllCustomers()
        //{            
        //    return await Task.FromResult(
        //        _mapper.Map<List<CustomerEntity>, List<CustomerModel>>(
        //            _context.Customer.ToList()));
        //}
    }
}
