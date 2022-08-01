using System.Text.Json.Serialization;

namespace FMFT.Web.Shared.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public Guid ConfirmEmailSecret { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTimeOffset? ConfirmEmailSendDate { get; set; }
        public DateTime CreateDate { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public UserInfo ToUserInfo()
        {
            return new UserInfo()
            {
                Id = Id,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Role = Role
            };
        }
    }
}
