using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

namespace FMFT.Web.Client.Services.Orchestrations.AccountReservations
{
    public interface IAccountReservationOrchestrationService
    {
        ValueTask<Reservation> CreateAccountReservationsAsync(CreateReservationModel model);
    }
}