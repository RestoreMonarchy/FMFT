using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using FMFT.Web.Client.Services.Pages;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

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

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;

        public List<Seat> OriginalSelectedSeats { get; set; }
        private int MaxSeatsAmount => OrderStateData.Items.Sum(x => x.Quantity);
        private bool HasSelectedSeats => MaxSeatsAmount == OrderStateData.SeatIds.Count;

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

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetAuditoriumResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            if (ShowResponse.IsSuccessful && AuditoriumResponse.IsSuccessful)
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

        private async Task<List<Seat>> GetSelectedSeatsAsync()
        {
            List<Seat> seats = new();
            List<int> originalSeatIds = OrderStateData.SeatIds.ToList();

            foreach (int seatId in originalSeatIds)
            {
                Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Id == seatId);
                
                // remove seat ids that don't exist
                if (seat == null)
                {
                    OrderStateData.SeatIds.Remove(seatId);
                    continue;
                }

                // remove seat ids that are already reserved
                if (Show.ReservedSeats.Any(x => x.SeatId == seatId))
                {
                    OrderStateData.SeatIds.Remove(seatId);
                    continue;
                }

                seats.Add(seat);
            }

            if (originalSeatIds.Count != OrderStateData.SeatIds.Count)
            {
                await UpdateOrderStateDataAsync();
            }

            return seats;
        }

        private Task HandleSelectedSeatsChangedAsync(List<Seat> seats)
        {
            OrderStateData.SeatIds = seats.Select(x => x.Id).ToList();
            InvokeAsync(() => UpdateOrderStateDataAsync());
            
            return Task.CompletedTask;
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
