using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account.Reservations
{
    public partial class AccountReservationPage
    {
        [Parameter]
        public string ReservationId { get; set; }

        public LoadingView LoadingView { get; set; }
        public ModalDialog CancelModalDialog { get; set; }
        public ButtonBase CancelButton { get; set; }
        public ModalDialog QRCodeModalDialog { get; set; }
        public ModalDialog SeatQRCodeModalDialog { get; set; }

        public APIResponse<Reservation> ReservationResponse { get; set; }

        public Reservation Reservation => ReservationResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsAuthenticated)
                return;

            ReservationResponse = await APIBroker.GetReservationByIdAsync(ReservationId);
            LoadingView.StopLoading();
        }

        public ReservationItem SelectedReservationSeat { get; set; }
        public APIResponse<QRCodeImage> SelectedReservationSeatQRCodeResponse { get; set; }
        public LoadingView SelectedReservationSeatQRCodeLoadingView { get; set; }
        private async Task HandleSeatQRCodeDialogAsync(ReservationItem reservationSeat)
        {
            SelectedReservationSeat = reservationSeat;

            SelectedReservationSeatQRCodeLoadingView.StartLoading();

            await SeatQRCodeModalDialog.ShowAsync();

            SelectedReservationSeatQRCodeResponse = await APIBroker.GetReservationSeatQRCodeAsync(Reservation.Id, reservationSeat.Id);

            SelectedReservationSeatQRCodeLoadingView.StopLoading();
        }

        private async Task HandleCancelDialogAsync()
        {
            await CancelModalDialog.ShowAsync();
        }

        public APIResponse<QRCodeImage> ReservationQRCodeResponse { get; set; }
        public LoadingView ReservationQRCodeLoadingView { get; set; }
        private async Task HandleQRCodeDialogAsync()
        {
            ReservationQRCodeLoadingView.StartLoading();

            await QRCodeModalDialog.ShowAsync();

            ReservationQRCodeResponse = await APIBroker.GetReservationQRCodeImageByIdAsync(Reservation.Id);

            ReservationQRCodeLoadingView.StopLoading();
        }

        private async Task HandleCancelAsync()
        {
            CancelButton.StartSpinning();

            CancelUserReservationRequest request = new()
            {
                UserId = UserAccountState.UserAccount.UserId,
                ReservationId = ReservationId
            };
            ReservationResponse = await APIBroker.CancelUserReservationAsync(request);
             
            await CancelModalDialog.HideAsync();
        }
    }
}
