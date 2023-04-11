using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
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

        private void HandleNextButton()
        {
            if (OrderStateData.Items.Where(x => !GetShowProduct(x.ShowProductId)?.IsBulk ?? true).Sum(x => x.Quantity) > 0)
            {
                NavigationBroker.NavigateTo(GetUrl("seats"));
            }
            else
            {
                NavigationBroker.NavigateTo(GetUrl("payment"));
            }
        }

        [Inject]
        public OrderingPageService OrderingPageService { get; set; }

        public LoadingView LoadingView { get; set; }

        public OrderStateData OrderStateData { get; set; }
        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }
        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }
        public APIResponse<List<Reservation>> UserReservationsResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;
        public List<Reservation> UserReservations => UserReservationsResponse.Object;
        public IEnumerable<Reservation> ActiveUserReservations => UserReservations.Where(x => x.Status == ReservationStatus.Ok);

        private string NextDisabled()   
        {
            int quantity = OrderStateData.Items.Sum(x => x.Quantity);

            return quantity == 0 ? "disabled" : string.Empty;
        }

        private int GetShowProductQuantity(ShowProduct showProduct)
        {
            int quantity;
            if (showProduct.IsBulk)
            {
                quantity = showProduct.Quantity - Show.ReservedBulkItems.Count();
            } else
            {
                quantity = Auditorium.Seats.Count - Show.ReservedSeats.Count();
            }

            return Math.Min(quantity, MaxQuantity);
        }

        private ShowProduct GetShowProduct(int showProductId)
        {
            return ShowProducts.FirstOrDefault(x => x.Id == showProductId);
        }

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsAuthenticated || !UserAccountState.IsEmailConfirmed)
            {
                return;
            }

            OrderStateData = await OrderingPageService.RetrieveOrderStateDataAsync(ShowId);

            if (!OrderStateData.IsValid())
            {
                OrderStateData = OrderingPageService.GetDefaultOrderStateData(ShowId);
                await OrderingPageService.SaveOrderStateDataAsync(OrderStateData);
            }

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetAuditoriumResponseAsync(),
                GetShowProductsResponseAsync(),
                GetUserReservationsResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            if (ShowResponse.IsSuccessful && ShowProductsResponse.IsSuccessful && UserReservationsResponse.IsSuccessful)
            {
                if (Show.IsPast() || Show.IsSellDisabled())
                {
                    NavigationBroker.NavigateTo($"/shows/{ShowId}");
                    return;
                }

                bool removed = false;
                foreach (OrderItemStateData orderItem in OrderStateData.Items.ToList())
                {
                    ShowProduct showProduct = GetShowProduct(orderItem.ShowProductId);
                    if (GetShowProductQuantity(showProduct) <= 0)
                    {
                        OrderStateData.Items.Remove(orderItem);
                        removed = true;
                    }
                }

                if (removed)
                {
                    await UpdateOrderStateDataAsync();
                }

            }
            
            LoadingView.StopLoading();
        }

        private async Task GetShowResponseAsync()
        {
            if (UserAccountState.IsInRole(UserRole.Admin))
            {
                ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            }
            else
            {
                ShowResponse = await APIBroker.GetPublicShowByIdAsync(ShowId);
            }
        }

        private async Task GetAuditoriumResponseAsync()
        {
            AuditoriumResponse = await APIBroker.GetAuditoriumByShowIdAsync(ShowId);
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
                    ShowProductId = showProduct.Id,
                    SeatIds = new()
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
