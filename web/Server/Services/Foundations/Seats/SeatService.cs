using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Seats.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Seats
{
    public class SeatService : ISeatService
    {
        private readonly IStorageBroker storageBroker;

        public SeatService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Seat> RetrieveSeatByIdAsync(int seatId)
        {
            Seat seat = await storageBroker.SelectSeatByIdAsync(seatId);
            if (seat == null)
            {
                throw new SeatNotFoundException();
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
