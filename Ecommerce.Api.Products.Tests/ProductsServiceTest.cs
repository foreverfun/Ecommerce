using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Api.Products.Data;
using Ecommerce.Api.Products.Models;
using Ecommerce.Api.Products.Profiles;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;

namespace Ecommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductModelProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
                
            var productRepo = new ProductRepo(dbContext, mapper, null);
            var product = await productRepo.GetPorductsAsync();

            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductModelProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productRepo = new ProductRepo(dbContext, mapper, null);
            var product = await productRepo.GetProductAsync(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductModelProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productRepo = new ProductRepo(dbContext, mapper, null);
            var product = await productRepo.GetProductAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            for (int i=1; i<=10; i++)
            {
                dbContext.Product.Add(
                    new ProductEntity()
                    {
                        Id = i,
                        Name = Guid.NewGuid().ToString(),
                        Inventory = i + 10,
                        Price = (decimal)(i * 3.14)
                    }
               );                   
            }
            dbContext.SaveChanges();
        }
    }
}
