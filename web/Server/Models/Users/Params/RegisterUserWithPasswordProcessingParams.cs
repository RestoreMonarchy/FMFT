namespace FMFT.Web.Server.Models.Users.Params
{
    public class RegisterUserWithPasswordProcessingParams
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordText { get; set; }
    }
}
