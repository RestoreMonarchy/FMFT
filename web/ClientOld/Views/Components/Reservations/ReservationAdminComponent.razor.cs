using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Auditoriums.Exceptions;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Exceptions;
using FMFT.Web.Client.Services.Views.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Reservations
{
    public partial class ReservationAdminComponent
    {
        [Parameter]
        public int ReservationId { get; set; }

        [Inject]
        public IReservationViewService ReservationViewService { get; set; }

        public Reservation Reservation { get; set; }
        public Auditorium Auditorium { get; set; }

        public Exception Exception { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Reservation = await ReservationViewService.RetrieveReservationByIdAsync(ReservationId);
                Auditorium = await ReservationViewService.RetrieveAuditoriumByIdAsync(Reservation.Show.AuditoriumId);
            } catch (ReservationNotFoundException exception)
            {
                Exception = exception;
            } catch (AuditoriumNotFoundException exception)
            {
                Exception = exception;
            }
        }
    }
}
