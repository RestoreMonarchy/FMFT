using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Users;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService
    {
        public Account MapUserToAccount(User user)
        {
            return new Account()
            {
                UserId = user.Id
            };
        }
    }
}
