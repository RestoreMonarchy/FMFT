using FMFT.Web.Shared.Models.Seats;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Seat> RetrieveSeatByIdAsync(int seatId);
        ValueTask<IEnumerable<Seat>> RetrieveAllSeatsAsync();
    }
}
