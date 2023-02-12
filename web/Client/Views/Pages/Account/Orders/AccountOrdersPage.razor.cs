using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Views.Pages.Account.Orders
{
    public partial class AccountOrdersPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Order>> OrdersResponse { get; set; }

        public List<Order> Orders => OrdersResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            OrdersResponse = await APIBroker.GetOrdersByUserIdAsync(UserAccountState.UserAccount.UserId);

            LoadingView.StopLoading();
        }

        public string GetOrderClasses(Order order)
        {
            List<string> classes = new();

            if (order.Status == OrderStatus.Expired)
            {
                classes.Add("list-group-item-light");
            }
            else
            {
                classes.Add("");
            }

            return string.Join(", ", classes);
        }
    }
}
