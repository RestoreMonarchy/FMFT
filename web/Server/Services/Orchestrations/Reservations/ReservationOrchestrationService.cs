﻿using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.QRCodes;
using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.QRCodes.Params;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations.Results;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Foundations.QRCodes;
using FMFT.Web.Server.Services.Foundations.Reservations;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public partial class ReservationOrchestrationService : IReservationOrchestrationService
    {
        private readonly IReservationService reservationService;
        private readonly IAccountService accountService;
        private readonly IQRCodeService qrCodeService;
        private readonly ILoggingBroker loggingBroker;

        public ReservationOrchestrationService(
            IReservationService reservationService,
            IAccountService accountService,
            ILoggingBroker loggingBroker,
            IQRCodeService qrCodeService)
        {
            this.reservationService = reservationService;
            this.accountService = accountService;
            this.loggingBroker = loggingBroker;
            this.qrCodeService = qrCodeService;
        }

        public async ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid)
        {
            return await qrCodeService.GenerateGuidQRCodeImageAsync(guid);
        }

        public async ValueTask<QRCodeImage> GenerateReservationTicketAsync(GenerateReservationTicketParams @params)
        {
            return await qrCodeService.GenerateReservationTicketAsync(@params);
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
        {
            return await reservationService.CreateReservationAsync(@params);
        }

        // TODO: To be remade anyways
        public async ValueTask<Reservation> UpdateReservationStatusAsync(UpdateReservationStatusRequest request)
        {
            //await accountService.AuthorizeAccountByRoleAsync(UserRole.Admin);

            UpdateReservationStatusParams @params = new()
            {
                ReservationId = request.ReservationId,
                ReservationStatus = request.Status,
                UpdateStatusDate = DateTimeOffset.Now,
                //AdminUserId = account.UserId
            };

            return await reservationService.UpdateReservationStatusAsync(@params);
        }

        public async ValueTask<Reservation> CancelReservationAsync(string reservationId)
        {
            return await reservationService.CancelReservationAsync(reservationId);
        }

        public async ValueTask<ValidateReservationSecretCodeResult> ValidateReservationSecretCodeAsync(Guid secretCode)
        {
            return await reservationService.ValidateReservationSecretCodeAsync(secretCode);
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserIdAsync(int userId)
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByUserIdAsync(userId);

            return reservations;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByShowIdAsync(showId);

            return reservations;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveAllReservationsAsync()
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveAllReservationsAsync();

            return reservations;
        }

        public async ValueTask<Reservation> RetrieveReservationByIdAsync(string reservationId)
        {
            Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);

            return reservation;
        }
    }
}
