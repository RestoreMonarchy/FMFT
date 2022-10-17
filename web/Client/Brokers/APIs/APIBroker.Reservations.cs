using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ReservationsRelativeUrl = "api/reservations";

        public async ValueTask<APIResponse<Reservation>> GetReservationByIdAsync(int reservationId)
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

        public async ValueTask<APIResponse<List<Reservation>>> GetUserReservationsAsync(int userId)
        {
            string url = $"api/users/{userId}/reservations";
            return await GetAsync<List<Reservation>>(url);
        }
    }
}
