using FMFT.Extensions.EmailClients.Models;
using System.Net;
using System.Net.Mail;

namespace FMFT.Extensions.EmailClients
{
    public class SmtpEmailClient : IEmailClient
    {
        private readonly SmtpEmailClientOptions options;
        private readonly SmtpClient client;

        public SmtpEmailClient(SmtpEmailClientOptions options)
        {
            this.options = options;
            client = new SmtpClient(options.Host, options.Port)
            {
                Credentials = new NetworkCredential(options.Email, options.Password)
            };
        }

        public async Task SendHtmlEmailMessageAsync(HtmlEmailMessage emailMessage)
        {
            MailMessage mailMessage = new()
            {
                Subject = emailMessage.Subject,
                From = new MailAddress(options.Email)
            };

            mailMessage.To.Add(emailMessage.EmailAddress);
            mailMessage.Body = emailMessage.Html;
            mailMessage.IsBodyHtml = true;

            await client.SendMailAsync(mailMessage);
        }
    }
}
