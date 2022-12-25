using FMFT.Web.Server.Models.Emails.Params;
using Hangfire;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public partial class EmailService
    {
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

        [AutomaticRetry(Attempts = 5)]
        public async Task SendRegisterEmailJobAsync(string emailAddress, RegisterEmailParams @params)
        {
            await SendRegisterEmailAsync(emailAddress, @params);
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendRegisterExternalEmailJobAsync(string emailAddress, RegisterExternalEmailParams @params)
        {
            await SendRegisterExternalEmailAsync(emailAddress, @params);
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task SendResetPasswordEmailJobAsync(string emailAddress, ResetPasswordEmailParams @params)
        {
            await SendResetPasswordEmailAsync(emailAddress, @params);
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendReservationSummaryEmailJobAsync(string emailAddress, ReservationSummaryEmailParams @params)
        {
            await SendReservationSummaryEmailAsync(emailAddress, @params);
        }        
    }
}
