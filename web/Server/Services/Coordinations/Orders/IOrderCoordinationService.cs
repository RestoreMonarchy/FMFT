﻿using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;

namespace FMFT.Web.Server.Services.Coordinations.Orders
{
    public interface IOrderCoordinationService
    {
        ValueTask<Order> CreateOrderAsync(CreateOrderParams @params);
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
    }
}