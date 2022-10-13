using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Services.Processings.AccountStores
{
    public interface IAccountStoreProcessingService
    {
        UserAccount RetrieveAccount();
        void UpdateAccount(UserAccount account);
    }
}