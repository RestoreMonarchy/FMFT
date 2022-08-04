using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService
    {
        public Account MapUserToAccount(User user)
        {
            return new Account()
            {
                UserId = user.Id,
                Name = string.Join(" ", user.FirstName, user.LastName),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }
    }
}
