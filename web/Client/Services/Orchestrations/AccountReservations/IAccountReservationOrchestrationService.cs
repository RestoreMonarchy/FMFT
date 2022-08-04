using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Models;

namespace FMFT.Web.Client.Services.Orchestrations.AccountReservations
{
    public interface IAccountReservationOrchestrationService
    {
        ValueTask<Reservation> CreateAccountReservationsAsync(CreateReservationModel model);
    }
}