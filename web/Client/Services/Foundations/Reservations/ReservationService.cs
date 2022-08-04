using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Exceptions;
using FMFT.Web.Client.Models.Reservations.Requests;
using RESTFulSense.WebAssembly.Exceptions;

namespace FMFT.Web.Client.Services.Foundations.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IAPIBroker apiBroker;

        public ReservationService(IAPIBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            try
            {
                Reservation reservation = await apiBroker.GetReservationByIdAsync(reservationId);
                return reservation;
            } catch (HttpResponseNotFoundException)
            {
                throw new ReservationNotFoundException();
            }            
        }

        public async ValueTask<List<Reservation>> RetrieveAllReservationsAsync()
        {
            List<Reservation> reservations = await apiBroker.GetAllReservationsAsync();
            return reservations;
        }

        public async ValueTask<List<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            try
            {
                List<Reservation> reservations = await apiBroker.GetUserReservationsAsync(userId);
                return reservations;
            } catch (HttpResponseUnauthorizedException)
            {
                throw new ReservationUnauthorizedException();
            }
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request)
        {
            try
            {
                Reservation reservation = await apiBroker.CreateReservationAsync(request);
                return reservation;
            } catch (HttpResponseConflictException)
            {
                throw new SeatAlreadyReservedException();
            } catch (HttpResponseForbiddenException)
            {
                throw new UserAlreadyReservedException();
            } catch (HttpResponseUnauthorizedException)
            {
                throw new ReservationUnauthorizedException();
            }
        }
    }
}
