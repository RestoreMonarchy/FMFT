using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Payments;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        public async ValueTask<APIResponse> SendMockPaymentNotificationAsync(MockPaymentNotification notification)
        {
            string url = $"api/payment/notifications/{PaymentProvider.Mock}";

            return await PostAsync(url, notification);
        }
    }
}
