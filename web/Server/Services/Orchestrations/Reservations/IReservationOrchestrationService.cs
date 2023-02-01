using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.QRCodes.Params;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations.Results;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public interface IReservationOrchestrationService
    {
        ValueTask<Reservation> CancelReservationAsync(string reservationId);
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<Reservation> CreateUserReservationAsync(CreateUserReservationParams @params);
        ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid);
        ValueTask<QRCodeImage> GenerateReservationTicketAsync(ReservationSeat reservationSeat, Reservation reservation);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(string reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserAndShowIdAsync(int userId, int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByOrderIdAsync(int orderId);
        ValueTask<ValidateReservationSecretCodeResult> ValidateReservationSecretCodeAsync(Guid secretCode);
        ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusParams @params);
        ValueTask SendReservationSummaryEmailAsync(string emailAddress, Reservation reservation);
    }
}