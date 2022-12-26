using FMFT.Web.Shared.Enums;
using Newtonsoft.Json;

namespace FMFT.Web.Server.Models.UserAccounts
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public UserAuthenticationType AuthenticationType { get; set; }
        public CultureId CultureId { get; set; }
        public bool IsPasswordEnabled { get; set; }
        public bool IsEmailConfirmed { get; set; } 
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? ConfirmEmailSendDate { get; set; }

        [JsonIgnore]
        public Guid ConfirmEmailSecret { get; set; }        
    }
}
