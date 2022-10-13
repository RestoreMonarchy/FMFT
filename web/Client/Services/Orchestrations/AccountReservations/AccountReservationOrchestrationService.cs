using FMFT.Web.Client.Models.AccountReservations.Arguments;
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Requests;
using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Client.Services.Processings.AccountStores;
using FMFT.Web.Client.Services.Processings.Reservations;

namespace FMFT.Web.Client.Services.Orchestrations.AccountReservations
{
    public class AccountReservationOrchestrationService : IAccountReservationOrchestrationService
    {
        private readonly IAccountStoreProcessingService accountStoreService;
        private readonly IReservationProcessingService reservationService;

        public AccountReservationOrchestrationService(
            IAccountStoreProcessingService accountStoreService, 
            IReservationProcessingService reservationService)
        {
            this.accountStoreService = accountStoreService;
            this.reservationService = reservationService;
        }

        public async ValueTask<List<Reservation>> RetrieveAccountReservationsAsync()
        {
            UserAccount account = accountStoreService.RetrieveAccount();

            return await reservationService.RetrieveReservationsByUserIdAsync(account.UserId);
        }

        public async ValueTask<Reservation> CreateAccountReservationAsync(CreateAccountReservationArguments arguments)
        {
            UserAccount account = accountStoreService.RetrieveAccount();

            CreateReservationRequest request = new()
            {
                SeatId = arguments.SeatId,
                ShowId = arguments.ShowId,
                UserId = account.UserId
            };

            return await reservationService.CreateReservationAsync(request);
        }
    }
}
