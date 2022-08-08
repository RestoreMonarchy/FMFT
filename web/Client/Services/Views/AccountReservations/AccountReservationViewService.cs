using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Services.Orchestrations.AccountReservations;

namespace FMFT.Web.Client.Services.Views.AccountReservations
{
    public class AccountReservationViewService : IAccountReservationViewService
    {
        private readonly IAccountReservationOrchestrationService accountReservationService;

        public AccountReservationViewService(IAccountReservationOrchestrationService accountReservationService)
        {
            this.accountReservationService = accountReservationService;
        }

        public async ValueTask<List<Reservation>> RetrieveAccountReservationsAsync()
        {
            return await accountReservationService.RetrieveAccountReservationsAsync();
        }
    }
}
