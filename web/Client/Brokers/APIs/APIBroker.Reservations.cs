using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Models;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ReservationsRelativeUrl = "api/reservations";

        public async ValueTask<Reservation> GetReservationByIdAsync(int reservationId)
        {
            return await GetAsync<Reservation>($"{ReservationsRelativeUrl}/{reservationId}");
        }

        public async ValueTask<List<Reservation>> GetAllReservationsAsync()
        {
            return await GetAsync<List<Reservation>>(ReservationsRelativeUrl);
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model)
        {
            string url = $"api/users/{model.UserId}/reservations/create";
            return await PostAsync<CreateReservationModel, Reservation>(url, model);
        }

        public async ValueTask<List<Reservation>> GetUserReservationsAsync(int userId)
        {
            string url = $"api/users/{userId}/reservations";
            return await GetAsync<List<Reservation>>(url);
        }
    }
}
