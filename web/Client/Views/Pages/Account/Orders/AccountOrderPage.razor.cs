using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account.Orders
{
    public partial class AccountOrderPage
    {
        [Parameter]
        public int OrderId { get; set; }

        public LoadingView LoadingView { get; set; }

        public APIResponse<Order> OrderResponse { get; set; }
        public APIResponse<List<Reservation>> OrderReservationsResponse { get; set; }

        public Order Order => OrderResponse.Object;
        public List<Reservation> OrderReservations => OrderReservationsResponse.Object;
        private Dictionary<OrderStatus, string> OrderStatusDescriptions { get; } = new()
        {
            { OrderStatus.Completed, "Zamówienie zostało zrealizowane" },
            { OrderStatus.PaymentReceived, "Płatność za zamówienie została zarejestrowana" },
            { OrderStatus.Expired, "Minął czas na dokonanie płatności. Zamówienie wygasło" },
            { OrderStatus.PaymentWaiting, "Dokończ dokonywanie płatności na stronie operatora przed upływem czasu" }
        };

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            OrderResponse = await APIBroker.GetOrderByIdAsync(OrderId);
            OrderReservationsResponse = await APIBroker.GetOrderReservationsByIdAsync(OrderId);

            if (OrderResponse.IsSuccessful)
            {
                // Expired but actually not
                if (Order.IsExpired && Order.Status == OrderStatus.PaymentWaiting)
                {
                    Order.Status = OrderStatus.Expired;
                }
            } 

            LoadingView.StopLoading();
        }

        private void HandleTimeElapsed()
        {
            Order.IsExpired = true;
            Order.Status = OrderStatus.Expired;
        }
    }
}
