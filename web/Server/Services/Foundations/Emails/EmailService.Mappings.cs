using FMFT.Extensions.EmailClients.Models;
using FMFT.Web.Server.Models.Emails;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public partial class EmailService
    {
        public List<HtmlEmailMessageAttachment> MapEmailAttachmentsToHtmlEmailMessageAttachments(List<EmailAttachment> attachments)
        {
            return attachments.Select(x => new HtmlEmailMessageAttachment()
            {
                Name = x.Name,
                Content = x.Content,
                ContentType = x.ContentType
            }).ToList();
        }
    }
}
