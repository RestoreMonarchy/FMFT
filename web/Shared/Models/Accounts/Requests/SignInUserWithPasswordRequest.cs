namespace FMFT.Web.Shared.Models.Accounts.Requests
{
    public class SignInUserWithPasswordRequest
    {
        public string Email { get; set; }
        public string PasswordText { get; set; }
        public bool IsPersistent { get; set; }
    }
}
