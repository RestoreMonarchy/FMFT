using FMFT.Web.Server.Brokers.Storages;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IStorageBroker storageBroker;

        public ReservationService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
    }
}
