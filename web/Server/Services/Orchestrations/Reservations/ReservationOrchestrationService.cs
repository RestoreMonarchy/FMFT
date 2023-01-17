using FluentAssertions;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.QRCodes;
using FMFT.Web.Server.Models.Emails;
using FMFT.Web.Server.Models.Emails.Params;
using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.QRCodes.Params;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations.Results;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Services.Foundations.Emails;
using FMFT.Web.Server.Services.Foundations.QRCodes;
using FMFT.Web.Server.Services.Foundations.Reservations;
using MimeTypes;
using System.Net.Mail;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public partial class ReservationOrchestrationService : IReservationOrchestrationService
    {
        private readonly IReservationService reservationService;
        private readonly IQRCodeService qrCodeService;
        private readonly IEmailService emailService;

        public ReservationOrchestrationService(
            IReservationService reservationService,
            IQRCodeService qrCodeService,
            IEmailService emailService)
        {
            this.reservationService = reservationService;
            this.qrCodeService = qrCodeService;
            this.emailService = emailService;
        }

        public async ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid)
        {
            return await qrCodeService.GenerateGuidQRCodeImageAsync(guid);
        }

        public async ValueTask<QRCodeImage> GenerateReservationTicketAsync(ReservationSeat reservationSeat, Reservation reservation)
        {
            GenerateReservationTicketParams @params = new()
            {
                SecretCode = reservationSeat.SecretCode,
                ShowName = reservation.Show.Name,
                Date = reservation.Show.StartDateTime,
                ReservationId = reservation.Id,
                Number = reservationSeat.Seat.Number,
                Row = reservationSeat.Seat.Row
            };

            return await qrCodeService.GenerateReservationTicketAsync(@params);
        }

        public async ValueTask<Reservation> CreateUserReservationAsync(CreateUserReservationParams @params)
        {
            CreateUserReservationValidationException validationException = new();

            const int maximumSeats = 3;

            if (@params.SeatIds.Count > maximumSeats)
            {
                validationException.UpsertDataList("SeatIds", $"The maximum amount of seats that can be in a reservation is {maximumSeats}");
            }

            validationException.ThrowIfContainsErrors();

            CreateReservationParams @params2 = new()
            {
                ShowId = @params.ShowId,
                UserId = @params.UserId,
                SeatIds  = @params.SeatIds
            };


            return await CreateReservationAsync(@params2);
        }

        public async ValueTask<Reservation> CreateReservationAsync(CreateReservationParams @params)
        {
            Reservation reservation = await reservationService.CreateReservationAsync(@params);
                        
            string emailAddress = reservation.User?.Email ?? reservation.Details?.Email ?? null;

            if (!string.IsNullOrEmpty(emailAddress))
            {
                await SendReservationSummaryEmailAsync(emailAddress, reservation);
            }

            return reservation;
        }

        private async ValueTask SendReservationSummaryEmailAsync(string emailAddress, Reservation reservation)
        {
            ReservationSummaryEmailParams @params = MapReservationToReservationSummaryEmailParams(reservation);
            foreach (ReservationSeat reservationSeat in reservation.Seats)
            {
                QRCodeImage qrCodeImage = await GenerateReservationTicketAsync(reservationSeat, reservation);
                @params.Attachments.Add(new EmailAttachment()
                {
                    Name = $"{reservation.Id}-r{reservationSeat.Seat.Row}-m{reservationSeat.Seat.Number}.{MimeTypeMap.GetExtension(qrCodeImage.ContentType)}",
                    ContentType = qrCodeImage.ContentType,
                    Content = qrCodeImage.Data
                });
            }

            await emailService.EnqueueSendReservationSummaryAsync(emailAddress, @params);
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
        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByOrderIdAsync(int orderId)
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByOrderIdAsync(orderId);

            return reservations;
        }
        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByShowIdAsync(int showId)
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByShowIdAsync(showId);

            return reservations;
        }

        public async ValueTask<IEnumerable<Reservation>> RetrieveReservationsByUserAndShowIdAsync(int userId, int showId)
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByUserAndShowIdAsync(userId, showId);

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
