using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public partial class ReservationService : TheStandardService, IReservationService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ReservationService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
            => TryCatch(async () =>
            {
                return await storageBroker.SelectAllReservationsAsync();
            });

        public ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
            => TryCatch(async () =>
            {
                return await storageBroker.SelectReservationsByUserIdAsync(userId);
            });

        public ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
            => TryCatch(async () =>
            {
                return await storageBroker.SelectReservationsByShowIdAsync(showId);
            });

        public ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId)
            => TryCatch(async () =>
            {
                Reservation reservation = await storageBroker.SelectReservationByIdAsync(reservationId);
                if (reservation == null)
                {
                    throw new NotFoundReservationException();
                }

                return reservation;
            });

        public ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
            => TryCatch(async () =>
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
            });

        public ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusParams @params)
            => TryCatch(async () =>
            {
                StoredProcedureResult<Reservation> result = await storageBroker.UpdateReservationStatusAsync(@params);

                if (result.ReturnValue == 1)
                {
                    throw new NotFoundReservationException();
                }

                return result.Result;
            });
    }
}
