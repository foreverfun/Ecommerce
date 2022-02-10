using Ecommerce.Api.Customers.Data;
using Ecommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Profiles
{
    public class CustomerModelProfile : AutoMapper.Profile
    {
        public CustomerModelProfile()
        {
            CreateMap<CustomerEntity, CustomerModel>();
        }
    }
}
