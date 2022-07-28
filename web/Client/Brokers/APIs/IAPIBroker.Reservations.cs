using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Reservation> GetReservationByIdAsync(int reservationId);
        ValueTask<List<Reservation>> GetAllReservationsAsync();
        ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model);
        ValueTask<List<Reservation>> GetUserReservationsAsync(int userId);
    }
}
