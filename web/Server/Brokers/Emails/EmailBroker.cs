using FMFT.Emails.Server.Models;
using FMFT.Emails.Server.Services;
using FMFT.Extensions.EmailClients;
using FMFT.Extensions.EmailClients.Models;
using FMFT.Web.Server.Models.Emails;
using FMFT.Web.Server.Models.Options.Emails;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace FMFT.Web.Server.Brokers.Emails
{
    public class EmailBroker : IEmailBroker
    {
        private readonly ServerEmailGenerator emailGenerator;
        private readonly SmtpEmailOptions options;
        private readonly IEmailClient emailClient;
        
        public EmailBroker(ServerEmailGenerator emailGenerator, IOptions<SmtpEmailOptions> options)
        {
            this.emailGenerator = emailGenerator;
            this.options = options.Value;

            emailClient = GetEmailClient();
        }

        private SmtpEmailClient GetEmailClient()
        {
            SmtpEmailClientOptions clientOptions = new()
            {
                Host = options.Host,
                Port = options.Port,
                SenderEmail = options.Email,
                Password = options.Password,
                SenderName = options.SenderName
            };

            return new SmtpEmailClient(clientOptions);
        }

        public async Task SendRegisterEmailAsync(Email<RegisterEmailModel> email)
        {
            HtmlEmailMessage message = new()
            {
                Subject = email.Subject,
                EmailAddress = email.EmailAddress
            };

            message.Html = await emailGenerator.GenerateRegisterEmailHtmlAsync(email.Model);

            await emailClient.SendHtmlEmailMessageAsync(message);
        }

        public async Task SendResetPasswordEmailAsync(Email<ResetPasswordEmailModel> email)
        {
            HtmlEmailMessage message = new()
            {
                Subject = email.Subject,
                EmailAddress = email.EmailAddress
            };

            message.Html = await emailGenerator.GenerateResetPasswordEmailHtmlAsync(email.Model);

            await emailClient.SendHtmlEmailMessageAsync(message);
        }
    }
}
