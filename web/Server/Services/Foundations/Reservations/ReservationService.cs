using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public partial class ReservationService : IReservationService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ReservationService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
        {
            return await storageBroker.SelectAllReservationsAsync();
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            return await storageBroker.SelectReservationsByUserIdAsync(userId);
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
        {
            return await storageBroker.SelectReservationsByShowIdAsync(showId);
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(string reservationId)
        {
            Reservation reservation = await storageBroker.SelectReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new NotFoundReservationException();
            }

            return reservation;
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
        {
            StoredProcedureResult<Reservation> result = await storageBroker.CreateReservationAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new SeatAlreadyReservedReservationException();
            }

            if (result.ReturnValue == 2)
            {
                throw new UserAlreadyReservedReservationException();
            }

            return result.Result;
        }

        public async ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusParams @params)
        {
            StoredProcedureResult<Reservation> result = await storageBroker.UpdateReservationStatusAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundReservationException();
            }

            return result.Result;
        }
    }
}
