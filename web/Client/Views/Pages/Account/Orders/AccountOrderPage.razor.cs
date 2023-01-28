using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Reservations;
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

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            OrderResponse = await APIBroker.GetOrderByIdAsync(OrderId);
            OrderReservationsResponse = await APIBroker.GetOrderReservationsByIdAsync(OrderId);

            LoadingView.StopLoading();
        }
    }
}
