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
                Credentials = new NetworkCredential(options.SenderEmail, options.Password),
                EnableSsl = true
            };
        }

        public async Task SendHtmlEmailMessageAsync(HtmlEmailMessage emailMessage)
        {
            MailMessage mailMessage = new()
            {
                Subject = emailMessage.Subject,
                From = new MailAddress(options.SenderEmail, options.SenderName)
            };

            mailMessage.To.Add(emailMessage.EmailAddress);
            mailMessage.Body = emailMessage.Html;
            mailMessage.IsBodyHtml = true;

            if (emailMessage.Attachments != null)
            {
                foreach (HtmlEmailMessageAttachment emailMessageAttachment in emailMessage.Attachments)
                {
                    MemoryStream contentStream = new(emailMessageAttachment.Content);
                    contentStream.Seek(0, SeekOrigin.Begin);

                    Attachment attachment = new(contentStream,
                        emailMessageAttachment.Name,
                        emailMessageAttachment.ContentType);

                    mailMessage.Attachments.Add(attachment);
                }
            }

            await client.SendMailAsync(mailMessage);
        }
    }
}
