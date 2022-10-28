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
        [Parameter]
        public int ShowId { get; set; }

        private string ShowName => ShowResponse?.Object?.Name ?? ShowId.ToString();

        private LoadingView LoadingView { get; set; }
        private Stepper Stepper { get; set; }
        public ButtonBase ConfirmButton { get; set; }

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }
        public APIResponse<Reservation> ReservationResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            if (ShowResponse.IsSuccessfull)
            {
                AuditoriumResponse = await APIBroker.GetAuditoriumByIdAsync(Show.AuditoriumId);
            }
            LoadingView.StopLoading();
        }

        public Panzoom AuditoriumPanzoom { get; set; }

        public Seat SelectedSeat { get; set; }

        private Task NextToConfirmAsync()
        {
            Stepper.StepUp();
            return Task.CompletedTask;
        }

        private async Task HandleConfirmAsync()
        {
            ConfirmButton.StartSpinning();

            CreateReservationRequest request = new()
            {
                ShowId = ShowId,
                SeatId = SelectedSeat.Id,
                UserId = UserAccountState.UserAccount.UserId
            };

            ReservationResponse = await APIBroker.CreateReservationAsync(request);

            Stepper.StepUp();
            Stepper.LockPast();
        }
    }
}
