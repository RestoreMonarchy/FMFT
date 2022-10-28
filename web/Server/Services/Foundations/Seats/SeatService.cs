using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Seats.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Seats
{
    public partial class SeatService : ISeatService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public SeatService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Seat> RetrieveSeatByIdAsync(int seatId)
        {
            Seat seat = await storageBroker.SelectSeatByIdAsync(seatId);
            if (seat == null)
            {
                throw new NotFoundSeatException();
            }

            return seat;
        }

        public async ValueTask<IEnumerable<Seat>> RetrieveAllSeatsAsync()
        {
            IEnumerable<Seat> seats = await storageBroker.SelectAllSeatsAsync();

            return seats;
        }
    }
}
