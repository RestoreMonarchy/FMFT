using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Accounts
{
    public class Account
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
    }
}
