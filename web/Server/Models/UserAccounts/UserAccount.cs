using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.UserAccounts
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public CultureId CultureId { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
