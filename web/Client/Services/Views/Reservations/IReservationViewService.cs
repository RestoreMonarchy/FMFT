using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;

namespace FMFT.Web.Client.Services.Views.Reservations
{
    public interface IReservationViewService
    {
        ValueTask<List<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId);
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
    }
}
