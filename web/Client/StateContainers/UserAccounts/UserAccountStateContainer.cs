using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.StateContainers.UserAccounts
{
    public class UserAccountStateContainer : IUserAccountStateContainer
    {
        private UserAccount userAccount;

        public UserAccount UserAccount
        {
            get
            {
                if (!IsAuthenticated)
                {
                    throw new Exception("User is not authenticated");
                }

                return userAccount;
            }
            set
            {
                userAccount = value;
                NotifyStateChanged();
            }
        }

        public bool IsAuthenticated => userAccount != null;
        public bool IsEmailConfirmed => userAccount?.IsEmailConfirmed ?? false;

        public bool IsInRole(UserRole userRole)
        {
            if (!IsAuthenticated)
            {
                return false;
            }

            return userAccount.Role == userRole;
        }

        public event Action OnChange;

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
