namespace FMFT.Web.Server.Models.Emails
{
    public class EmailAttachment
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
