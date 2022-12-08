using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.QRCodes.Params;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Results;
using FMFT.Web.Server.Models.Seats.Exceptions;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Coordinations.Reservations
{
    public class ReservationCoordinationService : IReservationCoordinationService
    {
        private readonly IReservationOrchestrationService reservationService;
        private readonly IUserAccountOrchestrationService userAccountService;

        public ReservationCoordinationService(
            IReservationOrchestrationService reservationService, 
            IUserAccountOrchestrationService userAccountService)
        {
            this.reservationService = reservationService;
            this.userAccountService = userAccountService;
        }

        public async ValueTask<QRCodeImage> GenerateReservationQRCodeImageAsync(string reservationId)
        {
            Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(reservation.User.Id, UserRole.Admin);

            return await reservationService.GenerateGuidQRCodeImageAsync(reservation.SecretCode);
        }

        public async ValueTask<QRCodeImage> GenerateReservationSeatQRCodeImageAsync(string reservationId, int reservationSeatId)
        {
            Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(reservation.User.Id, UserRole.Admin);

            ReservationSeat reservationSeat = reservation.Seats.FirstOrDefault(x => x.Id == reservationSeatId);

            if (reservationSeat == null)
            {
                throw new NotFoundSeatReservationException();
            }

            return await reservationService.GenerateGuidQRCodeImageAsync(reservationSeat.SecretCode);
        }

        public async ValueTask<QRCodeImage> GenerateReservationSeatTicketAsync(string reservationId, int reservationSeatId)
        {
            Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(reservation.User.Id, UserRole.Admin);

            ReservationSeat reservationSeat = reservation.Seats.FirstOrDefault(x => x.Id == reservationSeatId);

            if (reservationSeat == null)
            {
                throw new NotFoundSeatReservationException();
            }

            GenerateReservationTicketParams @params = new()
            {
                SecretCode = reservationSeat.SecretCode,
                ShowName = reservation.Show.Name,
                Date = reservation.Show.StartDateTime,
                ReservationId = reservation.Id,
                Number = reservationSeat.Seat.Number,
                Row = reservationSeat.Seat.Row
            };

            return await reservationService.GenerateReservationTicketAsync(@params);
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
        {
            return await reservationService.CreateReservationAsync(@params);
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(userId, UserRole.Admin);

            return await reservationService.RetrieveReservationsByUserIdAsync(userId);
        }

        public async ValueTask<Reservation> CancelReservationAsync(string reservationId)
        {
            Reservation reservation = await RetrieveReservationByIdAsync(reservationId);

            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(reservation.User.Id, UserRole.Admin);

            return await reservationService.CancelReservationAsync(reservationId);
        }

        public async ValueTask<ValidateReservationSecretCodeResult> ValidateReservationSecretCodeAsync(Guid secretCode)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            ValidateReservationSecretCodeResult result = await reservationService.ValidateReservationSecretCodeAsync(secretCode);

            return result;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await reservationService.RetrieveReservationsByShowIdAsync(showId);
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(string reservationId)
        {
            Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

            await userAccountService.AuthorizeUserAccountByUserIdOrRolesAsync(reservation.User.Id, UserRole.Admin);

            return reservation;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await reservationService.RetrieveAllReservationsAsync();
        }
    }
}
