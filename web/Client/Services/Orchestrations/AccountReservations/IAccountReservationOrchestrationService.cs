using FMFT.Web.Client.Models.AccountReservations.Arguments;
using FMFT.Web.Client.Models.Reservations;

namespace FMFT.Web.Client.Services.Orchestrations.AccountReservations
{
    public interface IAccountReservationOrchestrationService
    {
        ValueTask<Reservation> CreateAccountReservationAsync(CreateAccountReservationArguments arguments);
        ValueTask<IEnumerable<Reservation>> RetrieveAccountReservationsAsync();
    }
}