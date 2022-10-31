using FMFT.Emails.Server.Models;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public interface IEmailService
    {
        ValueTask SendRegisterEmailAsync(string emailAddress, RegisterEmailModel model);
    }
}