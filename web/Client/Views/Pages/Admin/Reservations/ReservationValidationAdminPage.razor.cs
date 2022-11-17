using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Reservations
{
    public partial class ReservationValidationAdminPage
    {
        public string ReservationId { get; set; }

        public TextInputBase SearchInput { get; set; }

        public APIResponse<Reservation> ReservationResponse { get; set; }

        public Reservation Reservation => ReservationResponse.Object;

        private bool isLoading = false;

        private async Task HandleSubmitAsync()
        {
            isLoading = true;
            SearchInput.Disable();

            string reservationId = ReservationId.TrimStart(' ').TrimEnd(' ');

            ReservationResponse = await APIBroker.GetReservationByIdAsync(reservationId);

            await SearchInput.SelectAsync();

            SearchInput.Enable();
            isLoading = false;
        }
    }
}
