namespace FMFT.Web.Server.Models.Emails.Params
{
    public class ResetPasswordEmailParams
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public TimeSpan ExpireTime { get; set; }
        public Guid SecretKey { get; set; }
    }
}
