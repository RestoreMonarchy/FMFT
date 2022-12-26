using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Models.Emails.Params;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public interface IEmailService
    {
        ValueTask EnqueueSendConfirmAccountEmailAsync(string emailAddress, ConfirmAccountEmailParams @params);
        ValueTask EnqueueSendRegisterEmailAsync(string emailAddress, RegisterEmailParams @params);
        ValueTask EnqueueSendRegisterExternalEmailAsync(string emailAddress, RegisterExternalEmailParams @params);
        ValueTask EnqueueSendReservationSummaryAsync(string emailAddress, ReservationSummaryEmailParams @params);
        ValueTask EnqueueSendResetPasswordEmailAsync(string emailAddress, ResetPasswordEmailParams @params);
    }
}