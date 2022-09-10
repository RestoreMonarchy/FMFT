using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Users.Params
{
    public class RegisterUserWithPasswordParams
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string PasswordHash { get; set; }
    }
}
