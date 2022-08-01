using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Requests;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public interface IReservationOrchestrationService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
    }
}