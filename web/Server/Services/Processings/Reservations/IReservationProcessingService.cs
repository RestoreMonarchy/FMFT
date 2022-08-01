using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Requests;
using FMFT.Web.Shared.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Processings.Reservations
{
    public interface IReservationProcessingService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
    }
}