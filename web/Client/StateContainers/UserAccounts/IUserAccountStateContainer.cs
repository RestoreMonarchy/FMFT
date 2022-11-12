using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.StateContainers.UserAccounts
{
    public interface IUserAccountStateContainer
    {
        UserAccount UserAccount { get; set; }
        bool IsAuthenticated { get; }

        event Action OnChange;

        bool IsInRole(UserRole userRole);
    }
}