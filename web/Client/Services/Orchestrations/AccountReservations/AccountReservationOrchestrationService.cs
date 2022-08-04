using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.Reservations;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Models;

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

        public async ValueTask<IEnumerable<Reservation>> RetrieveAccountReservationsAsync()
        {
            throw new NotImplementedException();
            
        }

        public async ValueTask<Reservation> CreateAccountReservationsAsync(CreateReservationModel model)
        {
            int userId = accountService.Account.Id;
            model.UserId = userId;
            return await reservationService.CreateReservationAsync(model);
        }
    }
}
