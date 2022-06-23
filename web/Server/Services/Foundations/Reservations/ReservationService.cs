using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Exceptions;
using FMFT.Web.Shared.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IStorageBroker storageBroker;

        public ReservationService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
        {
            return await storageBroker.SelectAllReservationsAsync();
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            return await storageBroker.SelectReservationsByUserIdAsync(userId);
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
        {
            Reservation reservation = await storageBroker.SelectReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new ReservationNotFoundException();
            }

            return reservation;
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
        {
            StoredProcedureResult<Reservation> result = await storageBroker.CreateReservationAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new SeatAlreadyReservedException();
            }

            if (result.ReturnValue == 2)
            {
                throw new UserAlreadyReservedException();
            }

            return result.Result;
        }
    }
}
