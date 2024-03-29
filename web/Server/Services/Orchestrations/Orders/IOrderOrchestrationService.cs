﻿using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;

namespace FMFT.Web.Server.Services.Orchestrations.Orders
{
    public interface IOrderOrchestrationService
    {
        ValueTask<Order> CreateOrderAsync(CreateOrderParams @params);
        ValueTask<IEnumerable<Order>> RetrieveAllOrdersAsync();
        ValueTask<Order> RetrieveOrderByIdAsync(int orderId);
        ValueTask<Order> RetrieveOrderBySessionIdAsync(Guid sessionId);
        ValueTask<IEnumerable<Order>> RetrieveOrdersByUserIdAsync(int userId);
        ValueTask<Order> UpdateOrderPaymentTokenAsync(UpdateOrderPaymentTokenParams @params);
        ValueTask<Order> UpdateOrderStatusAsync(UpdateOrderStatusParams @params);
    }
}