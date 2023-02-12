using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Payments;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse> SendMockPaymentNotificationAsync(MockPaymentNotification notification);
    }
}
