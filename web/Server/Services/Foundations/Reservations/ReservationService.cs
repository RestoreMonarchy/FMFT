using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.DTOs;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Results;

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

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserAndShowIdAsync(int userId, int showId)
        {
            return await storageBroker.SelectReservationsByUserAndShowIdAsync(userId, showId);
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
            CreateReservationDTO dto = new()
            {
                ShowId = @params.ShowId,
                UserId = @params.UserId,
                Seats = @params.SeatIds != null ? string.Join(',', @params.SeatIds) : string.Empty,
                
                Email = @params.Email,
                FirstName = @params.FirstName,
                LastName = @params.LastName
            };

            StoredProcedureResult<Reservation> result = await storageBroker.CreateReservationAsync(dto);

            if (result.ReturnValue == 1)
            {
                throw new SeatAlreadyReservedReservationException();
            }

            if (result.ReturnValue == 2)
            {
                throw new UserAlreadyReservedReservationException();
            }

            if (result.ReturnValue == 3)
            {
                throw new SeatsNotProvidedReservationException();
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

        public async ValueTask<Reservation> CancelReservationAsync(string reservationId)
        {
            StoredProcedureResult<Reservation> result = await storageBroker.CancelReservationAsync(reservationId);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundReservationException();
            }

            if (result.ReturnValue == 2)
            {
                throw new AlreadyCanceledReservationException();
            }

            return result.Result;
        }

        public async ValueTask<ValidateReservationSecretCodeResult> ValidateReservationSecretCodeAsync(Guid secretCode)
        {
            StoredProcedureResult<ValidateReservationSecretCodeResult> result = await storageBroker.ValidateReservationSecretCodeAsync(secretCode);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundReservationException();
            }

            return result.Result;
        }
    }
}
