using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Account> GetAccountInfoAsync();
        ValueTask<Account> PostAccountLoginAsync(SignInUserWithPasswordRequest request);
        ValueTask<Account> PostAccountRegisterAsync(RegisterUserWithPasswordRequest request);
        ValueTask<Account> PostExternalLoginConfirmationAsync(ExternalLoginConfirmationRequest request);
    }
}
