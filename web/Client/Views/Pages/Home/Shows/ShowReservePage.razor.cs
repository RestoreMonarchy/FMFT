using BlazorPanzoom;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.Steppers;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Seats;
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
        public APIResponse<Reservation> ReservationResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;
        public Reservation Reservation => ReservationResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            if (ShowResponse.IsSuccessful)
            {
                AuditoriumResponse = await APIBroker.GetAuditoriumByIdAsync(Show.AuditoriumId);
            }
            LoadingView.StopLoading();
        }

        public int TicketsCount { get; set; } = 1;
        private string TicketsCountString()
        {
            string format = string.Empty;
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

            CreateReservationRequest request = new()
            {
                ShowId = ShowId,
                UserId = UserAccountState.UserAccount.UserId,
                SeatIds = SelectedSeats.Select(x => x.Id).ToList()
            };

            ReservationResponse = await APIBroker.CreateReservationAsync(request);

            Stepper.StepUp();
            Stepper.LockPast();
        }
    }
}
