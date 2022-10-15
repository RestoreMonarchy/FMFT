using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Brokers.MemoryStorages
{
    public partial class MemoryStorageBroker
    {
        private const string AccountKey = "Account";

        public UserAccount GetAccount()
        {
            return GetValue<UserAccount>(AccountKey);
        }

        public void SetAccount(UserAccount account)
        {
            SetValue(AccountKey, account);
        }
    }
}
