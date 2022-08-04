using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public interface IReservationService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
    }
}