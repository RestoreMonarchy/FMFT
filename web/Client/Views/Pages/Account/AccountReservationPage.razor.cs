using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account
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

        public ReservationSeat SelectedReservationSeat { get; set; }
        public APIResponse<QRCodeImage> SelectedReservationSeatQRCodeResponse { get; set; }
        public LoadingView SelectedReservationSeatQRCodeLoadingView { get; set; }
        private async Task HandleSeatQRCodeDialogAsync(ReservationSeat reservationSeat)
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

            CancelReservationRequest request = new()
            {
                UserId = UserAccountState.UserAccount.UserId,
                ReservationId = ReservationId
            };
            ReservationResponse = await APIBroker.CancelReservationAsync(request);
             
            await CancelModalDialog.HideAsync();
        }

        public ButtonBase DownloadTicketButton { get; set; }
        private async Task HandleDownloadTicketAsync()
        {
            int reservationSeatId = SelectedReservationSeat.Id;

            DownloadTicketButton.StartSpinning();

            APIResponse<QRCodeImage> response = await APIBroker.GetReservationSeatTicketAsync(Reservation.Id, reservationSeatId);

            if (response.IsSuccessful)
            {
                QRCodeImage qrCodeImage = response.Object;
                string fileName = $"{ReservationId}-{Reservation.Show.Name}-{reservationSeatId}.jpg";

                await JSRuntimeBroker.DownloadFromByteArrayAsync(qrCodeImage.Data, fileName, qrCodeImage.ContentType);
            }

            DownloadTicketButton.StopSpinning();
        }
    }
}
