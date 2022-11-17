using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Coordinations.Reservations
{
    public interface IReservationCoordinationService
    {
        ValueTask<Reservation> CancelReservationAsync(string reservationId);
        ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params);
        ValueTask<QRCodeImage> GenerateReservationQRCodeImageAsync(string reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(string reservationId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId);
        ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
    }
}