using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Brokers.Emails;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Models.Emails;
using FMFT.Web.Server.Models.Emails.Params;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBroker emailBroker;
        private readonly IUrlBroker urlBroker;

        public EmailService(IEmailBroker emailBroker, IUrlBroker urlBroker)
        {
            this.emailBroker = emailBroker;
            this.urlBroker = urlBroker;
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

            await emailBroker.SendRegisterEmailAsync(email);
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

            await emailBroker.SendResetPasswordEmailAsync(email);
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

            await emailBroker.SendRegisterExternalEmailAsync(email);
        }
    }
}
