using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Accounts
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public CultureId CultureId { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTimeOffset? ConfirmEmailSendDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
