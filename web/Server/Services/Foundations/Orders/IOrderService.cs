﻿using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;

namespace FMFT.Web.Server.Services.Foundations.Orders
{
    public interface IOrderService
    {
        ValueTask<Order> CreateOrderAsync(CreateOrderParams @params);
        ValueTask<IEnumerable<Order>> RetrieveAllOrdersAsync();
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
        ValueTask<IEnumerable<Order>> RetrieveOrdersByUserIdAsync(int userId);
        ValueTask<Order> UpdateOrderPaymentTokenAsync(UpdateOrderPaymentTokenParams @params);
    }
}