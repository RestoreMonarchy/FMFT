using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Brokers.MemoryStorages
{
    public partial interface IMemoryStorageBroker
    {
        UserAccount GetAccount();
        void SetAccount(UserAccount account);
    }
}
