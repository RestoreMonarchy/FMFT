using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

namespace FMFT.Web.Server.Services.Orchestrations.UserReservations
{
    public interface IUserReservationOrchestrationService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
    }
}