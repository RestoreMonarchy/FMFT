using FMFT.Web.Client.Models;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Reservations.Responses;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<Reservation>> GetReservationByIdAsync(string reservationId);
        ValueTask<APIResponse<List<Reservation>>> GetReservationsByUserAndShowIdAsync(int userId, int showId);
        ValueTask<APIResponse<List<Reservation>>> GetAllReservationsAsync();
        ValueTask<APIResponse<Reservation>> CreateUserReservationAsync(CreateUserReservationRequest request);
        ValueTask<APIResponse<Reservation>> CreateReservationAsync(CreateReservationRequest request);
        ValueTask<APIResponse<Reservation>> CancelUserReservationAsync(CancelUserReservationRequest request);
        ValueTask<APIResponse<Reservation>> CancelAdminReservationAsync(CancelAdminReservationRequest request);
        ValueTask<APIResponse<List<Reservation>>> GetUserReservationsAsync(int userId);
        ValueTask<APIResponse<QRCodeImage>> GetReservationQRCodeImageByIdAsync(string reservationId);
        ValueTask<APIResponse<QRCodeImage>> GetReservationSeatQRCodeAsync(string reservationId, int seatId);
        ValueTask<APIResponse<QRCodeImage>> GetReservationSeatTicketAsync(string reservationId, int seatId);
        ValueTask<APIResponse<ValidateReservationResponse>> ValidateReservationAsync(ValidateReservationRequest request);
    }
}
