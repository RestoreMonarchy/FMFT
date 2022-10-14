using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.UserAccounts;
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

        public UserAccount MapUserToUserAccount(User user)
        {
            return new UserAccount()
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                CultureId = user.CultureId,
                IsEmailConfirmed = user.IsEmailConfirmed,
                CreateDate = user.CreateDate
            };
        }
    }
}
