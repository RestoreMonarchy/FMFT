using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Reservation> GetReservationByIdAsync(int reservationId);
        ValueTask<List<Reservation>> GetAllReservationsAsync();
        ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request);
        ValueTask<List<Reservation>> GetUserReservationsAsync(int userId);
    }
}
