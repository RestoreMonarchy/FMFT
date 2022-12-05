using FMFT.Web.Client.Models;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ReservationsRelativeUrl = "api/reservations";

        public async ValueTask<APIResponse<Reservation>> GetReservationByIdAsync(string reservationId)
        {
            return await GetAsync<Reservation>($"{ReservationsRelativeUrl}/{reservationId}");
        }

        public async ValueTask<APIResponse<List<Reservation>>> GetAllReservationsAsync()
        {
            return await GetAsync<List<Reservation>>(ReservationsRelativeUrl);
        }

        public async ValueTask<APIResponse<Reservation>> CreateReservationAsync(CreateReservationRequest request)
        {
            string url = $"api/users/{request.UserId}/reservations/create";
            return await PostAsync<Reservation>(url, request);
        }

        public async ValueTask<APIResponse<Reservation>> CancelReservationAsync(CancelReservationRequest request)
        {
            string url = $"api/users/{request.UserId}/reservations/{request.ReservationId}/cancel";
            return await PostAsync<Reservation>(url, null);
        }

        public async ValueTask<APIResponse<List<Reservation>>> GetUserReservationsAsync(int userId)
        {
            string url = $"api/users/{userId}/reservations";
            return await GetAsync<List<Reservation>>(url);
        }

        public async ValueTask<APIResponse<QRCodeImage>> GetReservationQRCodeImageByIdAsync(string reservationId)
        {
            string url = $"{ReservationsRelativeUrl}/{reservationId}/qrcode";
            return await GetAsync<QRCodeImage>(url);
        }

        public async ValueTask<APIResponse<QRCodeImage>> GetReservationQRCodeImageBySeatIdAsync(string reservationId, int seatId)
        {
            string url = $"{ReservationsRelativeUrl}/{reservationId}/seats/{seatId}/qrcode";
            return await GetAsync<QRCodeImage>(url);
        }
    }
}
