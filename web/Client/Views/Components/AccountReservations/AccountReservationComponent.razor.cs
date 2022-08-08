using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Services.Views.AccountReservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.AccountReservations
{
    public partial class AccountReservationComponent
    {
        [Parameter]
        public int ReservationId { get; set; }

        [Inject]
        public IAccountReservationViewService AccountReservationViewService { get; set; }

        public Reservation Reservation { get; set; }
        public Auditorium Auditorium { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Reservation = await AccountReservationViewService.RetrieveReservationByIdAsync(ReservationId);
            Auditorium = await AccountReservationViewService.RetrieveAuditoriumByIdAsync(Reservation.Show.AuditoriumId);
        }
    }
}
