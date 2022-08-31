using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<IEnumerable<Reservation>> SelectAllReservationsAsync();
        ValueTask<IEnumerable<Reservation>> SelectReservationsByUserIdAsync(int userId);
        ValueTask<IEnumerable<Reservation>> SelectReservationsByShowIdAsync(int showId);
        ValueTask<Reservation> SelectReservationByIdAsync(int reservationId);
        ValueTask<StoredProcedureResult<Reservation>> CreateReservationAsync(CreateReservationParams @params);
    }
}
