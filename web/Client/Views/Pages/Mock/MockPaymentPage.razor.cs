using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Payments;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Mock
{
    public partial class MockPaymentPage
    {
        [Parameter]
        public string SessionId { get; set; }

        private Guid sessionId;

        public APIResponse<Order> OrderResponse { get; set; }

        public Order Order => OrderResponse.Object;

        public LoadingView LoadingView { get; set; }
        public ButtonBase PayButton { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!Guid.TryParse(SessionId, out Guid sessionId))
            {
                return;    
            }

            this.sessionId = sessionId;

            await GetOrderResponseAsync();

            LoadingView.StopLoading();
        }

        private async Task GetOrderResponseAsync()
        {
            OrderResponse = await APIBroker.GetOrderBySessionIdAsync(sessionId);
        }

        public async Task PayAsync()
        {
            PayButton.StartSpinning();

            MockPaymentNotification notification = new()
            {
                SessionId = sessionId
            };

            APIResponse response = await APIBroker.SendMockPaymentNotificationAsync(notification);

            if (response.IsSuccessful)
            {
                NavigationBroker.NavigateTo($"/account/orders/{Order.Id}");
            }
        }
    }
}
