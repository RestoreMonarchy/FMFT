using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using FMFT.Web.Client.Services.Pages;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Orders
{
    public partial class SeatsOrderShowPage
    {
        private const string PageKey = "seats";

        [Parameter]
        public int ShowId { get; set; }

        private string GetUrl(string subPage)
        {
            return $"/shows/{ShowId}/order/{subPage}";
        }

        private string BackUrl => GetUrl("products");
        private string NextUrl => GetUrl("payment");

        [Inject]
        public OrderingPageService OrderingPageService { get; set; }

        public LoadingView LoadingView { get; set; }

        public OrderStateData OrderStateData { get; set; }
        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }
        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;

        public List<Seat> OriginalSelectedSeats { get; set; }
        private int MaxSeatsAmount => OrderStateData.Items.Except(BulkItems).Sum(x => x.Quantity);
        private bool HasSelectedSeats => MaxSeatsAmount == OrderStateData.Items.Sum(x => x.SeatIds.Count);

        private IEnumerable<OrderItemStateData> BulkItems => 
            OrderStateData.Items.Where(x => GetShowProduct(x.ShowProductId)?.IsBulk ?? true);

        private ShowProduct GetShowProduct(int showProductId)
        {
            return ShowProducts.FirstOrDefault(x => x.Id == showProductId);
        }

        private string NextDisabled()
        {
            return HasSelectedSeats ? string.Empty : "disabled";
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
                NavigationBroker.NavigateTo(GetUrl("products"));
                return;
            }

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetAuditoriumResponseAsync(),
                GetShowProductsResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            if (ShowResponse.IsSuccessful && AuditoriumResponse.IsSuccessful && ShowProductsResponse.IsSuccessful)
            {
                if (Show.IsPast() || Show.IsSellDisabled())
                {
                    NavigationBroker.NavigateTo($"/shows/{ShowId}");
                    return;
                }

                OriginalSelectedSeats = await GetSelectedSeatsAsync();
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

        private async Task<List<Seat>> GetSelectedSeatsAsync()
        {
            List<Seat> seats = new();
            bool removed = false;

            foreach (OrderItemStateData orderItem in OrderStateData.Items)
            {
                foreach (int seatId in orderItem.SeatIds)
                {
                    Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Id == seatId);

                    // remove seat ids that don't exist
                    if (seat == null)
                    {
                        orderItem.SeatIds.Remove(seatId);
                        removed = true;
                        continue;
                    }

                    // remove seat ids that are already reserved
                    if (Show.ReservedSeats.Any(x => x.SeatId == seatId))
                    {
                        orderItem.SeatIds.Remove(seatId);
                        removed = true;
                        continue;
                    }

                    seats.Add(seat);
                }
            }

            if (removed)
            {
                await UpdateOrderStateDataAsync();
            }

            return seats;
        }

        private async Task HandleSelectedSeatsChangedAsync(List<Seat> seats)
        {
            foreach (OrderItemStateData orderItem in OrderStateData.Items)
            {
                orderItem.SeatIds.Clear();
            }

            foreach (Seat seat in seats)
            {
                foreach (OrderItemStateData orderItem in OrderStateData.Items)
                {
                    ShowProduct showProduct = GetShowProduct(orderItem.ShowProductId);
                    if (showProduct.IsBulk)
                    {
                        continue;
                    }

                    if (orderItem.Quantity > orderItem.SeatIds.Count)
                    {
                        orderItem.SeatIds.Add(seat.Id);
                        break;
                    }
                }
            }

            await UpdateOrderStateDataAsync();
        }

        private string TicketsCountString()
        {
            int seatsCount = MaxSeatsAmount;

            string format;
            if (seatsCount == 1)
            {
                format = "{0} miejsce";
            }
            else if (seatsCount > 1 && seatsCount < 5)
            {
                format = "{0} miejsca";
            }
            else
            {
                format = "{0} miejsc";
            }

            return string.Format(format, seatsCount);
        }

        private async Task UpdateOrderStateDataAsync()
        {
            OrderStateData.CurrentStepKey = PageKey;
            await OrderingPageService.SaveOrderStateDataAsync(OrderStateData);
        }
    }
}
