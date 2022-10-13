using FMFT.Web.Client.Models.Accounts;

namespace FMFT.Web.Client.Services.Foundations.AccountStores
{
    public interface IAccountStoreService
    {
        UserAccount RetrieveAccount();
        void UpdateAccount(UserAccount account);
    }
}