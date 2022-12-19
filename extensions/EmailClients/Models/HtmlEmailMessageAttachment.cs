namespace FMFT.Extensions.EmailClients.Models
{
    public class HtmlEmailMessageAttachment
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
