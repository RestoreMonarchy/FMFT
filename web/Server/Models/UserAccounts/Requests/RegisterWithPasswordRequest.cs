namespace FMFT.Web.Server.Models.UserAccounts.Requests
{
    public class RegisterWithPasswordRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordText { get; set; }
    }
}
