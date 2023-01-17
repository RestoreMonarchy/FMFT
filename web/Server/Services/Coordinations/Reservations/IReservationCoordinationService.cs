using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Results;

namespace FMFT.Web.Server.Services.Coordinations.Reservations
{
    public interface IReservationCoordinationService
    {
        ValueTask<Reservation> CancelAdminReservationAsync(string reservationId);
        ValueTask<Reservation> CancelUserReservationAsync(string reservationId);
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<Reservation> CreateUserReservationAsync(CreateUserReservationParams @params);
        ValueTask<QRCodeImage> GenerateReservationQRCodeImageAsync(string reservationId);
        ValueTask<QRCodeImage> GenerateReservationSeatQRCodeImageAsync(string reservationId, int reservationSeatId);
        ValueTask<QRCodeImage> GenerateReservationSeatTicketAsync(string reservationId, int reservationSeatId);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(string reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserAndShowIdAsync(int userId, int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByOrderIdAsync(int orderId);
        ValueTask<ValidateReservationSecretCodeResult> ValidateReservationSecretCodeAsync(Guid secretCode);
    }
}