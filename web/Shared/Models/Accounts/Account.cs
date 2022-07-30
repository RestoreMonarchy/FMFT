using FMFT.Web.Shared.Models.Users;

namespace FMFT.Web.Shared.Models.Accounts
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
    }
}
