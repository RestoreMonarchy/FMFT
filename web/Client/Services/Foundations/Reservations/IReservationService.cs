using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

namespace FMFT.Web.Client.Services.Foundations.Reservations
{
    public interface IReservationService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model);
        ValueTask<List<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
        ValueTask<List<Reservation>> RetrieveUserReservationsAsync(int userId);
    }
}