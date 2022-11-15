using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.API.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTimeOffset? ConfirmEmailSendDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public string FullName() => string.Join(' ', FirstName, LastName);
    }
}
