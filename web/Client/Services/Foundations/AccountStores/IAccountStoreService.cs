using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Services.Foundations.AccountStores
{
    public interface IAccountStoreService
    {
        Account RetrieveAccount();
        void UpdateAccount(Account account);
    }
}