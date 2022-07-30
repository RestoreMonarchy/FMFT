using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Users;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService
    {
        public Account MapUserToAccount(User user)
        {
            return new Account()
            {
                Id = user.Id,
                Name = string.Join(" ", user.FirstName, user.LastName),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }
    }
}
