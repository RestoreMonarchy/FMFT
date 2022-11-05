using FMFT.Web.Shared.Enums;
using System.Text.Json.Serialization;

namespace FMFT.Web.Server.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public CultureId CultureId { get; set; }
        public bool IsPasswordEnabled { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public UserAuthenticationType AuthenticationType { get; set; }
        public DateTimeOffset? ConfirmEmailSendDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string ExternalLoginKey { get; set; }
        [JsonIgnore]
        public Guid ConfirmEmailSecret { get; set; }
    }
}
