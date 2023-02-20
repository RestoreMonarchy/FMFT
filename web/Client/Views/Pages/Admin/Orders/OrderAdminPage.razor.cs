using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Orders
{
    public partial class OrderAdminPage
    {
        [Parameter]
        public int OrderId { get; set; }

        public APIResponse<Order> OrderResponse { get; set; }
        public APIResponse<List<Reservation>> OrderReservationsResponse { get; set; }

        public Order Order => OrderResponse.Object;
        public List<Reservation> OrderReservations => OrderReservationsResponse.Object;

        public LoadingView LoadingView { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsInRole(UserRole.Admin))
            {
                return;
            }

            Task[] getDataTasks = new Task[]
            {
                GetOrderResponse(),
                GetOrderReservationsResponse()
            };

            await Task.WhenAll(getDataTasks);


            LoadingView.StopLoading();

        }

        private async Task GetOrderResponse()
        {
            OrderResponse = await APIBroker.GetOrderByIdAsync(OrderId);
        }

        private async Task GetOrderReservationsResponse()
        {
            OrderReservationsResponse = await APIBroker.GetOrderReservationsByIdAsync(OrderId);
        }
    }
}
