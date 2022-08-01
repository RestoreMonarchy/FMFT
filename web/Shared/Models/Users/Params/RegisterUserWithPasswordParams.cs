using FMFT.Web.Shared.Models.Shared.Enums;

namespace FMFT.Web.Shared.Models.Users.Params
{
    public class RegisterUserWithPasswordParams
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordText { get; set; }
    }
}
