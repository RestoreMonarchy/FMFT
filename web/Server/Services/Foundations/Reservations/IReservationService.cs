using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public interface IReservationService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(string reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
        ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusParams @params);
    }
}