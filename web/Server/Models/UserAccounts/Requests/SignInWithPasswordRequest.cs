namespace FMFT.Web.Server.Models.UserAccounts.Requests
{
    public class SignInWithPasswordRequest
    {
        public string Email { get; set; }
        public string PasswordText { get; set; }
        public bool IsPersistent { get; set; }
    }
}
