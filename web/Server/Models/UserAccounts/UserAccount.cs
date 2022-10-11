using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Users;

namespace FMFT.Web.Server.Models.UserAccounts
{
    public class UserAccount
    {
        public Account Account { get; set; }
        public User User { get; set; }
    }
}
