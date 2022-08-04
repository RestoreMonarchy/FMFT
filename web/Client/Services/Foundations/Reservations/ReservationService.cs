using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Models;
using FMFT.Web.Server.Models.Users.Exceptions;
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

        public async ValueTask<List<Reservation>> RetrieveUserReservationsAsync(int userId)
        {
            try
            {
                List<Reservation> reservations = await apiBroker.GetUserReservationsAsync(userId);
                return reservations;
            } catch (HttpResponseUnauthorizedException)
            {
                // using Models.Users exception inside Reservation service
                throw new UserNotAuthorizedException();
            }
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationModel model)
        {
            try
            {
                Reservation reservation = await apiBroker.CreateReservationAsync(model);
                return reservation;
            } catch (HttpResponseConflictException)
            {
                throw new SeatAlreadyReservedException();
            } catch (HttpResponseForbiddenException)
            {
                throw new UserAlreadyReservedException();
            } catch (HttpResponseUnauthorizedException)
            {
                // using Models.Users exception inside Reservation service
                throw new UserNotAuthorizedException();
            }
        }
    }
}
