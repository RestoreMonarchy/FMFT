using Dapper;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.DTOs;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Reservations.DTOs;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Users;
using System.Data;
using System.Text.Json;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {

        public async ValueTask<IEnumerable<Order>> SelectAllOrdersAsync()
        {
            GetOrderParams @params = new();
            return await GetOrdersAsync(@params);
        }
        public async ValueTask<Order> SelectOrderByIdAsync(int orderId)
        {
            GetOrderParams @params = new()
            {
                OrderId = orderId
            };

            return await GetOrderAsync(@params);
        }
        public async ValueTask<IEnumerable<Order>> SelectOrdersByUserIdAsync(int userId)
        {
            GetOrderParams @params = new()
            {
                UserId = userId
            };

            return await GetOrdersAsync(@params);
        }

        public async ValueTask<StoredProcedureResult<Order>> CreateOrderAsync(CreateOrderDTO dto)
        {
            string createOrderJSON = JsonSerializer.Serialize(dto);

            const string sql = "dbo.CreateOrder";
            StoredProcedureResult<Order> result = new();

            DynamicParameters parameters = new();
            parameters.Add(name: "@Order", dbType: DbType.String, value: createOrderJSON);
            parameters.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            result.Result = await QueryOrderAsync(sql, parameters, CommandType.StoredProcedure);
            result.ReturnValue = GetReturnValue(parameters);

            return result;
        }

        public async ValueTask<StoredProcedureResult<Order>> UpdateOrderPaymentTokenAsync(UpdateOrderPaymentTokenParams @params)
        {
            const string sql = "dbo.UpdateOrderPaymentToken";

            StoredProcedureResult<Order> result = new();

            DynamicParameters parameters = new();
            parameters.Add(name: "@OrderId", dbType: DbType.Int32, value: @params.OrderId);
            parameters.Add(name: "@PaymentToken", dbType: DbType.String, value: @params.PaymentToken);
            parameters.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            result.Result = await QueryOrderAsync(sql, parameters, CommandType.StoredProcedure);
            result.ReturnValue = GetReturnValue(parameters);

            return result;
        }

        public async ValueTask<StoredProcedureResult<Order>> UpdateOrderStatusAsync(UpdateOrderStatusParams @params)
        {
            const string sql = "dbo.UpdateOrderStatus";

            StoredProcedureResult<Order> result = new();

            DynamicParameters parameters = new();
            parameters.Add(name: "@OrderId", dbType: DbType.Int32, value: @params.OrderId);
            parameters.Add(name: "@Status", dbType: DbType.Byte, value: @params.Status);
            parameters.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            result.Result = await QueryOrderAsync(sql, parameters, CommandType.StoredProcedure);
            result.ReturnValue = GetReturnValue(parameters);

            return result;
        }

        private async ValueTask<Order> GetOrderAsync(GetOrderParams @params)
        {
            IEnumerable<Order> orders = await GetOrdersAsync(@params);

            return orders.FirstOrDefault();
        }
        
        private async ValueTask<IEnumerable<Order>> GetOrdersAsync(GetOrderParams @params)
        {
            const string sql = "dbo.GetOrders";

            return await QueryOrdersAsync(sql, @params, CommandType.StoredProcedure);
        }

        private async ValueTask<Order> QueryOrderAsync(string sql, object param = null, CommandType? commandType = null)
        {
            Order order = null;

            await connection.QueryAsync<Order, UserInfo, OrderItem, ShowProduct, Show, Order>(sql, (o, u, i, sp, s) =>
            {
                if (order == null)
                {
                    order = o;
                    order.User = u;
                    order.Items = new();
                }

                if (i != null)
                {
                    i.ShowProduct = sp;
                    i.Show = s;
                    order.Items.Add(i);
                }

                return null;
            }, param, commandType: commandType);

            return order;
        }

        private async ValueTask<IEnumerable<Order>> QueryOrdersAsync(string sql, object param = null, CommandType? commandType = null)
        {
            List<Order> orders = new();

            await connection.QueryAsync<Order, UserInfo, OrderItem, ShowProduct, Show, Order>(sql, (o, u, i, sp, s) =>
            {
                Order order = orders.FirstOrDefault(x => x.Id == o.Id);

                if (order == null)
                {
                    order = o;
                    order.User = u;
                    order.Items = new();
                    orders.Add(order);
                }                

                if (i != null)
                {
                    i.ShowProduct = sp;
                    i.Show = s;
                    order.Items.Add(i);
                }

                return null;
            }, param, commandType: commandType);

            return orders;
        }
    }
}
