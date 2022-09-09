using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Requests;

namespace FMFT.Web.Server.Services.Orchestrations.AccountReservations
{
    public interface IAccountReservationOrchestrationService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
        ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusRequest request);
    }
}