using BlazorPanzoom;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.Steppers;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;
using System.Runtime.Intrinsics.X86;

namespace FMFT.Web.Client.Views.Pages.Home.Shows
{
    public partial class ShowReservePage
    {
        private const int MaxSeats = 3;

        [Parameter]
        public int ShowId { get; set; }

        private string ShowName => ShowResponse?.Object?.Name ?? ShowId.ToString();

        private LoadingView LoadingView { get; set; }
        private Stepper Stepper { get; set; }
        public ButtonBase ConfirmButton { get; set; }
        public ButtonBase BackButton { get; set; }

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }
        public APIResponse<List<Reservation>> UserReservationsResponse { get; set; }
        public APIResponse<Reservation> ReservationResponse { get; set; }
        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;
        public List<Reservation> UserReservations => UserReservationsResponse.Object;
        public Reservation Reservation => ReservationResponse.Object;
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;

        public Reservation UserReservation => UserReservations.OrderByDescending(x => x.CreateDate).FirstOrDefault(x => !x.IsCanceled);

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetAuditoriumResponseAsync(),
                GetUserReservationsResponseAsync(),
                GetShowProductsResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            LoadingView.StopLoading();
        }

        private async Task GetShowResponseAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
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
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            UserReservationsResponse = await APIBroker.GetReservationsByUserAndShowIdAsync(UserAccountState.UserAccount.UserId, ShowId);
        }



        public int TicketsCount { get; set; } = 1;
        private string TicketsCountString()
        {
            string format;
            if (TicketsCount == 1)
            {
                format = "{0} miejsce";
            } else if (TicketsCount > 1 && TicketsCount < 5)
            {
                format = "{0} miejsca";
            } else
            {
                format = "{0} miejsc";
            }

            return string.Format(format, TicketsCount);
        }

        private bool showSelectSeats = false;

        private void HandleShowSelectSeats()
        {
            SelectedSeats = new();
            showSelectSeats = true;            
        }

        private void HandleHideSelectSeats()
        {
            showSelectSeats = false;
        }

        private bool HasSelectedSeats => SelectedSeats.Count == TicketsCount;

        public List<Seat> SelectedSeats { get; set; } = new();

        private Task NextToConfirmAsync()
        {
            Stepper.StepUp();
            return Task.CompletedTask;
        }
        
        private Task BackToSelectAsync()
        {
            Stepper.StepDown();
            return Task.CompletedTask;
        }

        private async Task HandleConfirmAsync()
        {
            BackButton.Disable();
            ConfirmButton.StartSpinning();

            CreateUserReservationRequest request = new()
            {
                ShowId = ShowId,
                UserId = UserAccountState.UserAccount.UserId,
                SeatIds = SelectedSeats.Select(x => x.Id).ToList()
            };

            ReservationResponse = await APIBroker.CreateUserReservationAsync(request);

            Stepper.StepUp();
            Stepper.LockPast();
        }
    }
}
