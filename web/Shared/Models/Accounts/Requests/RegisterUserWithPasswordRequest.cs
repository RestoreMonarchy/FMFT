namespace FMFT.Web.Shared.Models.Accounts.Requests
{
    public class RegisterUserWithPasswordRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordText { get; set; }
    }
}
