using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Brokers.Emails;
using FMFT.Web.Server.Models.Emails;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBroker emailBroker;

        public EmailService(IEmailBroker emailBroker)
        {
            this.emailBroker = emailBroker;
        }

        public async ValueTask SendRegisterEmailAsync(string emailAddress, RegisterEmailModel model)
        {
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
