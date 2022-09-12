using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Services.Processings.AccountStores
{
    public interface IAccountStoreProcessingService
    {
        Account RetrieveAccount();
        void UpdateAccount(Account account);
    }
}