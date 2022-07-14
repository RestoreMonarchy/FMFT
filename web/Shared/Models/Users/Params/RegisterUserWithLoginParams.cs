namespace FMFT.Web.Shared.Models.Users.Params
{
    public class RegisterUserWithLoginParams
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
