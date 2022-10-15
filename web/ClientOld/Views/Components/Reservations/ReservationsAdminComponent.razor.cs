using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Services.Views.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Reservations
{
    public partial class ReservationsAdminComponent
    {
        [Inject]
        public IReservationViewService ReservationViewService { get; set; }

        public List<Reservation> Reservations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Reservations = await ReservationViewService.RetrieveAllReservationsAsync();
        }
    }
}
