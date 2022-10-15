using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Models.API.Accounts.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<UserAccount> GetUserAccountAsync();
        ValueTask<AccountToken> PostAccountLoginAsync(LogInWithPasswordRequest request);
        ValueTask<AccountToken> PostAccountRegisterAsync(RegisterWithPasswordRequest request);
    }
}
