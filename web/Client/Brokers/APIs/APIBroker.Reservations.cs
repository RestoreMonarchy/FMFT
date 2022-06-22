using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Models;

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
            return await PostAsync<CreateReservationModel, Reservation>($"{ReservationsRelativeUrl}/create", model);
        }
    }
}
