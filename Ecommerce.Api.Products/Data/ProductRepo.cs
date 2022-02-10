using AutoMapper;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Data
{
    public class ProductRepo: IProductRepo
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepo> _logger;        

        public ProductRepo(ProductDbContext context, IMapper mapper, ILogger<ProductRepo> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            SeedData();
        }

        private void SeedData()
        {            
            if (!_context.Product.Any())
            {
                List<ProductEntity> seedData = new()
                {
                    new() { Id = 1, Name = "Keyboard", Price = 20M, Inventory = 100 },
                    new() { Id = 2, Name = "Mouse", Price = 5M, Inventory = 200 },
                    new() { Id = 3, Name = "Monitor", Price = 150M, Inventory = 50 },
                    new() { Id = 4, Name = "CPU", Price = 200M, Inventory = 2000 },
                };
                _context.AddRange(seedData);
                _context.SaveChanges();
            }
        }

        //public async Task<List<ProductModel>> GetAllProducts()
        //{            
        //    return await Task.FromResult(
        //        _mapper.Map<List<ProductEntity>, List<ProductModel>>(
        //            _context.Product.ToList()));
        //}

        public async Task<(bool IsSuccess, ProductModel Product, string ErrorMessage )> GetProductAsync(int id)
        {
            try
            {
                _logger?.LogInformation($"Query products with id: {id}");
                var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
                if (product !=null)
                {
                    _logger?.LogInformation($"Product {id} found");
                    var result = _mapper.Map<ProductModel>(product);
                    return (true, result, null);
                }
                return (false, null, $"Product {id} Not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetPorductsAsync()
        {
            try
            {
                _logger?.LogInformation("Querying products");
                var products = await _context.Product.ToListAsync();
                if (products!=null && products.Any())
                {
                    _logger?.LogInformation($"{products.Count} Products found");
                    var result = _mapper.Map<IEnumerable<ProductModel>>(products);
                    return (true, result, null);
                }
                return (false, null, "No product is found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }            
        }
    }
}
