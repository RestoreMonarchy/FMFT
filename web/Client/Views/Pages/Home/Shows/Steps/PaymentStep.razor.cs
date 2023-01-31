using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Orders.Requests;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Steps
{
    public partial class PaymentStep
    {
        [Parameter]
        public OrderState OrderState { get; set; }
        [Parameter]
        public EventCallback<OrderState> OrderStateChanged { get; set; }
        [Parameter]
        public Show Show { get; set; }

        public ButtonBase PayButton { get; set; }
        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase ErrorAlert { get; set; }

        public IEnumerable<OrderItemState> OrderItems => OrderState.Items.Where(x => x.Quantity > 0);

        private bool IsPayButtonDisabled => !OrderState.IsAgreeTerms;

        protected override void OnParametersSet()
        {
            if (OrderState.PaymentMethod == PaymentMethod.None)
            {
                OrderState.PaymentMethod = PaymentMethod.Blik;
            }
        }

        private async Task InvokeOrderStateChangedAsync()
        {
            await OrderStateChanged.InvokeAsync(OrderState);
        }

        private async Task HandlePaymentMethodChangedAsync(PaymentMethod paymentMethod)
        {
            OrderState.PaymentMethod = paymentMethod;
            await InvokeOrderStateChangedAsync();
        }

        private async Task CreateOrderTmp()
        {
            AlertGroup.HideAll();
            PayButton.StartSpinning();

            CreateOrderRequest createOrderRequest = new()
            {
                Amount = OrderState.TotalPrice,
                UserId = UserAccountState.UserAccount.UserId,
                Currency = "PLN",
                PaymentMethod = OrderState.PaymentMethod,   
                SeatIds = OrderState.Seats.Select(x => x.Id).ToList(),
                Items = new()
            };

            foreach (OrderItemState orderItem in OrderState.Items)
            {
                if (orderItem.Quantity == 0)
                {
                    continue;
                }

                createOrderRequest.Items.Add(new CreateOrderRequest.Item()
                {
                    ShowProductId = orderItem.ShowProduct.Id,
                    Price = orderItem.ShowProduct.Price,
                    Quantity = orderItem.Quantity
                });
            }

            APIResponse<Order> response = await APIBroker.CreateOrderAsync(createOrderRequest);
            if (response.IsSuccessful)
            {
                Order order = response.Object;
                APIResponse<PaymentUrl> paymentResponse = await APIBroker.GetOrderPaymentUrlAsync(order.Id);
                NavigationBroker.NavigateTo(paymentResponse.Object.Url);
            } else
            {
                ErrorAlert.Show();
                PayButton.StopSpinning();
            }
        }
    }
}
