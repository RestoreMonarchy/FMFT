using FMFT.Web.Server.Models.Emails.Params;
using Hangfire;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public partial class EmailService
    {
        public ValueTask EnqueueSendConfirmAccountEmailAsync(string emailAddress, ConfirmAccountEmailParams @params)
        {
            BackgroundJob.Enqueue(() => SendConfirmAccountEmailJobAsync(emailAddress, @params));

            return ValueTask.CompletedTask;
        }

        public ValueTask EnqueueSendRegisterEmailAsync(string emailAddress, RegisterEmailParams @params)
        {
            BackgroundJob.Enqueue(() => SendRegisterEmailJobAsync(emailAddress, @params));

            return ValueTask.CompletedTask;
        }

        public ValueTask EnqueueSendRegisterExternalEmailAsync(string emailAddress, RegisterExternalEmailParams @params)
        {
            BackgroundJob.Enqueue(() => SendRegisterExternalEmailJobAsync(emailAddress, @params));

            return ValueTask.CompletedTask;
        }

        public ValueTask EnqueueSendResetPasswordEmailAsync(string emailAddress, ResetPasswordEmailParams @params)
        {
            BackgroundJob.Enqueue(() => SendResetPasswordEmailJobAsync(emailAddress, @params));

            return ValueTask.CompletedTask;
        }

        public ValueTask EnqueueSendReservationSummaryAsync(string emailAddress, ReservationSummaryEmailParams @params) 
        {
            BackgroundJob.Enqueue(() => SendReservationSummaryEmailJobAsync(emailAddress, @params));

            return ValueTask.CompletedTask;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task SendConfirmAccountEmailJobAsync(string emailAddress, ConfirmAccountEmailParams @params)
        {
            try 
            {
                await SendConfirmAccountEmailAsync(emailAddress, @params);
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Confirm Account Email couldn't be sent because '{emailAddress}' is not a valid email address.");
            }
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendRegisterEmailJobAsync(string emailAddress, RegisterEmailParams @params)
        {
            try
            {
                await SendRegisterEmailAsync(emailAddress, @params);
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Register Email couldn't be sent because '{emailAddress}' is not a valid email address.");
            }            
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendRegisterExternalEmailJobAsync(string emailAddress, RegisterExternalEmailParams @params)
        {
            try
            {
                await SendRegisterExternalEmailAsync(emailAddress, @params);
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Register External Email couldn't be sent because '{emailAddress}' is not a valid email address.");
            }            
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task SendResetPasswordEmailJobAsync(string emailAddress, ResetPasswordEmailParams @params)
        {
            try
            {
                await SendResetPasswordEmailAsync(emailAddress, @params);
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Reset Password Email couldn't be sent because '{emailAddress}' is not a valid email address.");
            }
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendReservationSummaryEmailJobAsync(string emailAddress, ReservationSummaryEmailParams @params)
        {
            try
            {
                await SendReservationSummaryEmailAsync(emailAddress, @params);
            } catch (FormatException)
            {
                loggingBroker.LogWarning($"Reservation Summary Email couldn't be sent because '{emailAddress}' is not a valid email address.");
            }
        }        
    }
}
