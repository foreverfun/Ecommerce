using Ecommerce.Api.Products.Data;
using Ecommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Profiles
{
    public class ProductModelProfile:AutoMapper.Profile
    {
        public ProductModelProfile()
        {
            CreateMap<ProductEntity,ProductModel>();
        }
    }
}
