using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Processings.Reservations
{
    public interface IReservationProcessingService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
    }
}