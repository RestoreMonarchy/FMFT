using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations;

namespace FMFT.Web.Client.Services.Views.AccountReservations
{
    public interface IAccountReservationViewService
    {
        ValueTask<List<Reservation>> RetrieveAccountReservationsAsync();
        ValueTask<Auditorium> RetrieveAuditoriumByIdAsync(int auditoriumId);
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
    }
}