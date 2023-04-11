using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Models.Services.Orders;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Pages
{
    public class OrderingPageService : PageServiceBase
    {
        private readonly IStorageBroker storageBroker;

        public OrderingPageService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public OrderStateData GetDefaultOrderStateData(int showId)
        {
            return new()
            {
                ShowId = showId,
                Items = new(),
                PaymentMethod = PaymentMethod.Blik,
                Version = OrderStateData.CurrentVersion,
                ExpireDate = DateTime.UtcNow.AddHours(2)
            };
        }

        public async ValueTask<OrderStateData> RetrieveOrderStateDataAsync(int showId)
        {
            OrderStateData orderStateData = await storageBroker.GetOrderStateDataAsync(showId);

            if (orderStateData == null)
            {
                orderStateData = GetDefaultOrderStateData(showId);
            } else
            {
                orderStateData.ExpireDate = DateTime.UtcNow.AddHours(2);
                await SaveOrderStateDataAsync(orderStateData);
            }

            return orderStateData;  
        }

        public async ValueTask SaveOrderStateDataAsync(OrderStateData orderStateData)
        {
            await storageBroker.SetOrderStateDataAsync(orderStateData.ShowId, orderStateData);
        }

        public async ValueTask ResetOrderStateDataAsync(int showId)
        {
            await storageBroker.RemoveOrderStateDataAsync(showId);
        }
    }
}
