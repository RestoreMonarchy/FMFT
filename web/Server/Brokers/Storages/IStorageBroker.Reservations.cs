using FMFT.Web.Server.Models.Database;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Reservation> GetReservationAsync(int reservationId);
        ValueTask<StoredProcedureResult<Reservation>> CreateReservationAsync(CreateReservationParams @params);
    }
}
