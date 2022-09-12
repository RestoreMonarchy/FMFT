using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Brokers.MemoryStorages
{
    public partial interface IMemoryStorageBroker
    {
        Account GetAccount();
        void SetAccount(Account account);
    }
}
