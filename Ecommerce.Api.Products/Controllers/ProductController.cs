using Ecommerce.Api.Products.Data;
using Ecommerce.Api.Products.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace Ecommerce.Api.Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetProductsAsync()
        //{
        //    var result = await _productRepo.GetPorductsAsync();

        //    if (result.IsSuccess)
        //    {
        //        Tuple<bool, IEnumerable<ProductModel>> data = new Tuple<bool, IEnumerable<ProductModel>>(true, result.Products);
        //        return Ok(data);
        //    }
        //    var errorData = new Tuple<bool, string>(false, result.ErrorMessage);
        //    return NotFound(errorData);
        //}

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productRepo.GetPorductsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productRepo.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound(result.ErrorMessage);
        }
    }
}
