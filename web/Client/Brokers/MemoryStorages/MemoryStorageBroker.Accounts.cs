using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Brokers.MemoryStorages
{
    public partial class MemoryStorageBroker
    {
        private const string AccountKey = "Account";

        public Account GetAccount()
        {
            return GetValue<Account>(AccountKey);
        }

        public void SetAccount(Account account)
        {
            SetValue(AccountKey, account);
        }
    }
}
