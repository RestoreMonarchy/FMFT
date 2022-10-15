using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Services.Views.AccountReservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.AccountReservations
{
    public partial class AccountReservationsComponent
    {
        [Inject]
        public IAccountReservationViewService AccountReservationViewService { get; set; }

        public List<Reservation> Reservations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Reservations = await AccountReservationViewService.RetrieveAccountReservationsAsync();
        }
    }
}
