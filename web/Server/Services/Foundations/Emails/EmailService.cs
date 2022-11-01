using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Brokers.Emails;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Models.Emails;
using FMFT.Web.Server.Models.Emails.Params;

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
    }
}
