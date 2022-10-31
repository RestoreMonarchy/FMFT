namespace FMFT.Web.Server.Models.Options.Emails
{
    public class SmtpEmailOptions
    {
        public const string SectionKey = "Email:SMTP";

        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
