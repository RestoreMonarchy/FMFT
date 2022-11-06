using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Users.Params
{
    public class RegisterUserWithLoginParams
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
        public string FriendlyName { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
