namespace FMFT.Web.Shared.Models.Users.Models
{
    public class SignInUserWithPasswordModel
    {
        public string Email { get; set; }
        public string PasswordText { get; set; }
        public bool IsPersistent { get; set; }
    }
}
