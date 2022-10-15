namespace FMFT.Web.Client.Models.Accounts.Requests
{
    public class LogInWithPasswordRequest
    {
        public string Email { get; set; }
        public string PasswordText { get; set; }
        public bool IsPersistent { get; set; }
    }
}
