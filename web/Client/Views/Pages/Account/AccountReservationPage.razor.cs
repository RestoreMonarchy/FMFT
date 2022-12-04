using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
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

        private async Task HandleSeatQRCodeDialogAsync()
        {
            await SeatQRCodeModalDialog.ShowAsync();
        }

        private async Task HandleCancelDialogAsync()
        {
            await CancelModalDialog.ShowAsync();
        }

        private async Task HandleQRCodeDialogAsync()
        {
            await QRCodeModalDialog.ShowAsync();
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
    }
}
