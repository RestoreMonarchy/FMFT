using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.Reservations;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

namespace FMFT.Web.Client.Services.Orchestrations.AccountReservations
{
    public class AccountReservationOrchestrationService : IAccountReservationOrchestrationService
    {
        private readonly IAccountProcessingService accountService;
        private readonly IReservationProcessingService reservationService;

        public AccountReservationOrchestrationService(
            IAccountProcessingService accountService, 
            IReservationProcessingService reservationService)
        {
            this.accountService = accountService;
            this.reservationService = reservationService;
        }

        public async ValueTask<Reservation> CreateAccountReservationsAsync(CreateReservationModel model)
        {
            return null;
        }
    }
}
