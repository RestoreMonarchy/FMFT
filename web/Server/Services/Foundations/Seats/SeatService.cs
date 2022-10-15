using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Seats.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Seats
{
    public partial class SeatService : TheStandardService, ISeatService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public SeatService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Seat> RetrieveSeatByIdAsync(int seatId)
            => TryCatch(async () =>
            {
                Seat seat = await storageBroker.SelectSeatByIdAsync(seatId);
                if (seat == null)
                {
                    throw new NotFoundSeatException();
                }

                return seat;
            });

        public ValueTask<IEnumerable<Seat>> RetrieveAllSeatsAsync()
            => TryCatch(async () =>
            {
                IEnumerable<Seat> seats = await storageBroker.SelectAllSeatsAsync();

                return seats;
            });
    }
}
