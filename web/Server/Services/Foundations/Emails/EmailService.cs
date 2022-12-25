using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Brokers.Emails;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Models.Emails;
using FMFT.Web.Server.Models.Emails.Params;
using Hangfire;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public partial class EmailService : IEmailService
    {
        private readonly IEmailBroker emailBroker;
        private readonly IUrlBroker urlBroker;
        private readonly ILoggingBroker loggingBroker;

        public EmailService(IEmailBroker emailBroker, IUrlBroker urlBroker, ILoggingBroker loggingBroker)
        {
            this.emailBroker = emailBroker;
            this.urlBroker = urlBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask SendRegisterEmailAsync(string emailAddress, RegisterEmailParams @params)
        {
            RegisterEmailModel model = new()
            {
                Email = @params.Email,
                FirstName = @params.FirstName,
                ConfirmLink = urlBroker.GetClientConfirmEmailUrl(@params.UserId, @params.ConfirmSecret)
            };

            Email<RegisterEmailModel> email = new()
            {
                Subject = "Potwierdzenie rejestracji",
                EmailAddress = emailAddress,
                Model = model
            };

            loggingBroker.LogInformation("Sending RegisterEmail message...");
            try 
            {
                await emailBroker.SendRegisterEmailAsync(email);
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Register Email couldn't be sent because '{email}' is not a valid email.");
            }
            
            loggingBroker.LogInformation("RegisterEmail message has been sent!");
        }

        public async ValueTask SendResetPasswordEmailAsync(string emailAddress, ResetPasswordEmailParams @params)
        {
            ResetPasswordEmailModel model = new()
            {
                Email = @params.Email,
                FirstName = @params.FirstName,
                ExpireTime = @params.ExpireTime,
                ResetLink = urlBroker.GetClientResetPasswordEmailUrl(@params.SecretKey)
            };

            Email<ResetPasswordEmailModel> email = new()
            {
                Subject = "Reset hasła",
                EmailAddress = emailAddress,
                Model = model
            };

            loggingBroker.LogInformation("Sending ResetPasswordEmail message...");
            try
            {
                await emailBroker.SendResetPasswordEmailAsync(email);
                loggingBroker.LogInformation("ResetPasswordEmail message has been sent!");
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Reset Password Email couldn't be sent because '{email}' is not a valid email.");
            }            
        }

        public async ValueTask SendRegisterExternalEmailAsync(string emailAddress, RegisterExternalEmailParams @params)
        {
            RegisterExternalEmailModel model = new()
            {
                Email = @params.Email,
                FirstName = @params.FirstName,
                AuthenticationMethod = @params.AuthenticationMethod
            };

            Email<RegisterExternalEmailModel> email = new()
            {
                Subject = "Potwierdzenie rejestracji",
                EmailAddress = emailAddress,
                Model = model
            };

            loggingBroker.LogInformation("Sending RegisterExternalEmail message...");
            try
            {
                await emailBroker.SendRegisterExternalEmailAsync(email);
                loggingBroker.LogInformation("RegisterExternalEmail message has been sent!");
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Register External Email couldn't be sent because '{email}' is not a valid email.");
            }
        }
        
        public async ValueTask SendReservationSummaryEmailAsync(string emailAddress, ReservationSummaryEmailParams @params)
        {
            ReservationSummaryEmailModel model = new()
            {
                FirstName = @params.FirstName,
                ShowName = @params.ShowName,
                ReservationId = @params.ReservationId,
                ReservationSeats = new()
            };

            foreach (ReservationSummaryEmailParams.ReservationSeat seat in  @params.ReservationSeats)
            {
                model.ReservationSeats.Add(new ReservationSummaryEmailModel.ReservationSeat()
                {
                    Row = seat.Row,
                    Number = seat.Number
                });
            }

            Email<ReservationSummaryEmailModel> email = new()
            {
                Subject = "Potwierdzenie rezerwacji",
                EmailAddress = emailAddress,
                Model = model,
                Attachments = MapEmailAttachmentsToHtmlEmailMessageAttachments(@params.Attachments)
            };

            loggingBroker.LogInformation("Sending ReservationSummaryEmail message...");
            try
            {
                await emailBroker.SendReservationSummaryEmailAsync(email);
                loggingBroker.LogInformation("ReservationSummaryEmail message has been sent!");
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Reservation Summary Email couldn't be sent because '{email}' is not a valid email.");
            }
        }
    }
}
