using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Models.Emails;

namespace FMFT.Web.Server.Brokers.Emails
{
    public interface IEmailBroker
    {
        Task SendConfirmAccountEmailAsync(Email<ConfirmAccountEmailModel> email);
        Task SendRegisterEmailAsync(Email<RegisterEmailModel> email);
        Task SendRegisterExternalEmailAsync(Email<RegisterExternalEmailModel> email);
        Task SendReservationSummaryEmailAsync(Email<ReservationSummaryEmailModel> email);
        Task SendResetPasswordEmailAsync(Email<ResetPasswordEmailModel> email);
    }
}
