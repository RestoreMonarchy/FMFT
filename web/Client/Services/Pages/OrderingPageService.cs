using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Models.Services.Orders;

namespace FMFT.Web.Client.Services.Pages
{
    public class OrderingPageService : PageServiceBase
    {
        private readonly IStorageBroker storageBroker;

        public OrderingPageService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<OrderStateData> RetrieveOrderStateDataAsync(int showId)
        {
            OrderStateData orderStateData = await storageBroker.GetOrderStateDataAsync(showId);

            if (orderStateData == null)
            {
                orderStateData = new()
                {
                    ShowId = showId,
                    Items = new(),
                    SeatIds = new()
                };
            }

            return orderStateData;  
        }

        public async ValueTask SaveOrderStateDataAsync(OrderStateData orderStateData)
        {
            await storageBroker.SetOrderStateDataAsync(orderStateData.ShowId, orderStateData);
        }
    }
}
