using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Forms;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;

namespace FMFT.Web.Client.Views.Pages.Staff
{
    public partial class ReservationsStaffPage
    {
        [Parameter]
        public string ReservationId { get; set; }

        public FormBase Form { get; set; }

        public APIResponse<Reservation> ReservationResponse { get; set; }

        public Reservation Reservation => ReservationResponse.Object;

        public string SearchString { get; set; }
        private bool isLoading = false;

        protected override async Task OnParametersSetAsync()
        {
            SearchString = ReservationId;
            ReservationResponse = null;
            isLoading = true;

            if (!string.IsNullOrEmpty(ReservationId))
            {
                await Task.Delay(1000);
                ReservationResponse = await APIBroker.GetReservationByIdAsync(ReservationId);
            }

            isLoading = false;
        }

        private async Task HandleSubmitAsync()
        {
            await Console.Out.WriteLineAsync("submit hello!");
            SearchString = SearchString.Trim();
            NavigationBroker.NavigateTo($"/staff/reservations/{SearchString}");
        }
    }
}
