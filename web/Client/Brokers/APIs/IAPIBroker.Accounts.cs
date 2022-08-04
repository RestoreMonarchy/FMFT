using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Account> GetAccountInfoAsync();
        ValueTask<Account> PostAccountLoginAsync(LogInWithPasswordRequest request);
        ValueTask<Account> PostAccountRegisterAsync(RegisterWithPasswordRequest request);
        ValueTask<Account> PostConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
    }
}
