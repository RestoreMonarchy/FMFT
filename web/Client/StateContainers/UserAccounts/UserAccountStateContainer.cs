using FMFT.Web.Client.Models.API.Accounts;

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

        public event Action OnChange;

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
