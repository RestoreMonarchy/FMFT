using FMFT.Web.Server.Models.Seats;

namespace FMFT.Web.Server.Services.Foundations.Seats
{
    public interface ISeatService
    {
        ValueTask<IEnumerable<Seat>> RetrieveAllSeatsAsync();
        ValueTask<Seat> RetrieveSeatByIdAsync(int seatId);
    }
}