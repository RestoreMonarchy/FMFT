namespace FMFT.Web.Server.Models.Users.Arguments
{
    public class RegisterUserWithPasswordArguments
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordText { get; set; }
    }
}
