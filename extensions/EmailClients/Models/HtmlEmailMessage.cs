namespace FMFT.Extensions.EmailClients.Models
{
    public class HtmlEmailMessage
    {
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public List<HtmlEmailMessageAttachment> Attachments { get; set; }
    }
}
