using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.DTOs;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Results;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<IEnumerable<Reservation>> SelectAllReservationsAsync();
        ValueTask<IEnumerable<Reservation>> SelectReservationsByUserIdAsync(int userId);
        ValueTask<IEnumerable<Reservation>> SelectReservationsByShowIdAsync(int showId);
        ValueTask<IEnumerable<Reservation>> SelectReservationsByUserAndShowIdAsync(int userId, int showId);
        ValueTask<Reservation> SelectReservationByIdAsync(string reservationId);
        ValueTask<StoredProcedureResult<Reservation>> CreateReservationAsync(CreateReservationDTO dto);
        ValueTask<StoredProcedureResult<Reservation>> UpdateReservationStatusAsync(UpdateReservationStatusParams @params);
        ValueTask<StoredProcedureResult<Reservation>> CancelReservationAsync(string reservationId);
        ValueTask<StoredProcedureResult<ValidateReservationSecretCodeResult>> ValidateReservationSecretCodeAsync(Guid secretCode);
    }
}
