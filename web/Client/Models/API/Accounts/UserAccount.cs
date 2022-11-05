using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.API.Accounts
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public CultureId CultureId { get; set; }
        public bool IsPasswordEnabled { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
