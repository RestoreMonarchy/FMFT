using Dapper;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Users;
using System.Data;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Order> SelectOrderByIdAsync(int orderId)
        {
            GetOrderParams @params = new()
            {
                OrderId = orderId
            };

            return await GetOrderAsync(@params);
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
                }

                orders.Add(order);

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
