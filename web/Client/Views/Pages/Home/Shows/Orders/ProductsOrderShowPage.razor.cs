using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using FMFT.Web.Client.Services.Pages;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Orders
{
    public partial class ProductsOrderShowPage
    {
        private const int MaxQuantity = 5;
        private const string PageKey = "products";

        [Parameter]
        public int ShowId { get; set; }

        private string GetUrl(string subPage)
             => $"/shows/{ShowId}/order/{subPage}";

        [Inject]
        public OrderingPageService OrderingPageService { get; set; }

        public LoadingView LoadingView { get; set; }

        public OrderStateData OrderStateData { get; set; }
        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }
        public APIResponse<List<Reservation>> UserReservationsResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;
        public List<Reservation> UserReservations => UserReservationsResponse.Object;
        public IEnumerable<Reservation> ActiveUserReservations => UserReservations.Where(x => x.Status == ReservationStatus.Ok);

        private string NextDisabled()   
        {
            int quantity = OrderStateData.Items.Sum(x => x.Quantity);

            return quantity == 0 ? "disabled" : string.Empty;
        }

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            OrderStateData = await OrderingPageService.RetrieveOrderStateDataAsync(ShowId);

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetShowProductsResponseAsync(),
                GetUserReservationsResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            LoadingView.StopLoading();
        }

        private async Task GetShowResponseAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
        }

        private async Task GetShowProductsResponseAsync()
        {
            ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(ShowId);
        }

        private async Task GetUserReservationsResponseAsync()
        {
            UserReservationsResponse = await APIBroker.GetReservationsByUserAndShowIdAsync(UserAccountState.UserAccount.UserId, ShowId);
        }

        public int GetQuantity(int showProductId)
        {
            OrderItemStateData orderItem = OrderStateData.Items.FirstOrDefault(x => x.ShowProductId == showProductId);

            return orderItem?.Quantity ?? 0;
        }

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (OrderItemStateData orderItem in OrderStateData.Items)
            {
                ShowProduct showProduct = ShowProducts.FirstOrDefault(x => x.Id == orderItem.ShowProductId);
                decimal price = showProduct?.Price ?? 0;

                totalPrice += price * orderItem.Quantity;
            }

            return totalPrice;
        }

        private Task HandleQuantityChangeAsync(ShowProduct showProduct, ChangeEventArgs args)
        {
            int.TryParse(args.Value.ToString(), out int quantity);

            if (quantity == 0)
            {
                int removedElements = OrderStateData.Items.RemoveAll(x => x.ShowProductId == showProduct.Id);
                if (removedElements > 0)
                {
                    InvokeAsync(() => UpdateOrderStateDataAsync());
                }

                return Task.CompletedTask;                
            }

            OrderItemStateData orderItem = OrderStateData.Items.FirstOrDefault(x => x.ShowProductId == showProduct.Id);
            
            if (orderItem == null)
            {
                orderItem = new()
                {
                    ShowId = showProduct.ShowId,
                    ShowProductId = showProduct.Id
                };
                OrderStateData.Items.Add(orderItem);
            }

            orderItem.Quantity = quantity;
            InvokeAsync(() => UpdateOrderStateDataAsync());

            return Task.CompletedTask;
        }

        private async Task HandleCancelAsync()
        {
            await OrderingPageService.ResetOrderStateDataAsync(ShowId);
            NavigationBroker.NavigateTo($"/shows/{ShowId}");
        }

        private async Task UpdateOrderStateDataAsync()
        {
            OrderStateData.CurrentStepKey = PageKey;
            await OrderingPageService.SaveOrderStateDataAsync(OrderStateData);
        }
    }
}
