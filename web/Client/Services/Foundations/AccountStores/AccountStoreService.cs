using FMFT.Web.Client.Brokers.MemoryStorages;
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Exceptions;

namespace FMFT.Web.Client.Services.Foundations.AccountStores
{
    public class AccountStoreService : IAccountStoreService
    {
        private readonly IMemoryStorageBroker memoryStorageBroker;

        public AccountStoreService(IMemoryStorageBroker memoryStorageBroker)
        {
            this.memoryStorageBroker = memoryStorageBroker;
        }

        public UserAccount RetrieveAccount()
        {
            UserAccount account = memoryStorageBroker.GetAccount();
            return account;
        }

        public void UpdateAccount(UserAccount account)
        {
            memoryStorageBroker.SetAccount(account);
        } 
    }
}
