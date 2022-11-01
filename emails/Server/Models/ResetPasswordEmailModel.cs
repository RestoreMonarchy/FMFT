namespace FMFT.Emails.Server.Models
{
    public class ResetPasswordEmailModel
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public TimeSpan ExpireTime { get; set; }
        public string ResetLink { get; set; }
    }
}
