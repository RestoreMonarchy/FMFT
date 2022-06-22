using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Exceptions;
using FMFT.Web.Shared.Models.Reservations.Models;
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

        public async ValueTask<List<Reservation>> RetrieveAllReservationsASync()
        {
            List<Reservation> reservations = await apiBroker.GetAllReservationsAsync();
            return reservations;
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
            }
        }
    }
}
