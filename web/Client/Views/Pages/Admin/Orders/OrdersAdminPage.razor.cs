using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Orders
{
    public partial class OrdersAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Order>> OrdersResponse { get; set; }

        public List<Order> Orders => OrdersResponse.Object;

        private string searchString = string.Empty;

        private IEnumerable<Order> SearchOrders => FilterOrders
            .Where(x => string.IsNullOrEmpty(searchString) || 
            x.Id.ToString().Equals(searchString, StringComparison.OrdinalIgnoreCase));

        private IEnumerable<Order> FilterOrders => Orders
            .Where(x => StatusFilters[x.Status])
            .OrderByDescending(x => x.CreateDate);

        private Dictionary<OrderStatus, bool> StatusFilters = new()
        {
            { OrderStatus.PaymentWaiting, false },
            { OrderStatus.PaymentReceived, true },
            { OrderStatus.Completed, true },
            { OrderStatus.Expired, false }
        };

        protected override async Task OnInitializedAsync()
        {
            OrdersResponse = await APIBroker.GetAllOrdersAsync();

            LoadingView.StopLoading();
        }

        private string StatusFilterId(OrderStatus status)
        {
            return $"statusfilter-{status}";
        }

        private void ChangeStatusFilter(OrderStatus status, ChangeEventArgs args)
        {
            bool value = bool.Parse(args.Value.ToString());
            StatusFilters[status] = value;
            StateHasChanged();
        }

        private string GetClass(Order order)
        {
            List<string> classes = new();

            switch (order.Status)
            {
                case OrderStatus.PaymentWaiting:
                    break;
                case OrderStatus.PaymentReceived:
                    classes.Add("table-primary");
                    break;
                case OrderStatus.Completed:
                    classes.Add("table-success");
                    break;
                case OrderStatus.Expired:
                    classes.Add("table-warning");
                    break;
            }

            return string.Join(' ', classes);
        }
    }
}
