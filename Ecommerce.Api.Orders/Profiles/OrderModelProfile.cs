using Ecommerce.Api.Orders.Data;
using Ecommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Profiles
{
    public class OrderModelProfile : AutoMapper.Profile
    {
        public OrderModelProfile()
        {
            CreateMap<OrderEntity, OrderModel>();
        }
    }
}
