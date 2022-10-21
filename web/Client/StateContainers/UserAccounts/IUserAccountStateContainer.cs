using FMFT.Web.Client.Models.API.Accounts;

namespace FMFT.Web.Client.StateContainers.UserAccounts
{
    public interface IUserAccountStateContainer
    {
        UserAccount UserAccount { get; set; }
        bool IsAuthenticated { get; }

        event Action OnChange;
    }
}