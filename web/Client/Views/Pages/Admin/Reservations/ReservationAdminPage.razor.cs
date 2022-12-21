using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Reservations
{
    public partial class ReservationAdminPage
    {
        [Parameter]
        public string ReservationId { get; set; }

        public LoadingView LoadingView { get; set; }
        public ModalDialog CancelModalDialog { get; set; }
        public ButtonBase CancelButton { get; set; }

        public APIResponse<Reservation> ReservationResponse { get; set; }

        public Reservation Reservation => ReservationResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ReservationResponse = await APIBroker.GetReservationByIdAsync(ReservationId);
            LoadingView.StopLoading();
        }

        private async Task HandleCancelDialogAsync()
        {
            await CancelModalDialog.ShowAsync();
        }

        private async Task HandleCancelAsync()
        {
            CancelButton.StartSpinning();

            CancelAdminReservationRequest request = new()
            {
                ReservationId = ReservationId
            };
            ReservationResponse = await APIBroker.CancelAdminReservationAsync(request);

            await CancelModalDialog.HideAsync();
        }
    }
}
