using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Models.AccountTokens;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<UserAccount> GetUserAccountAsync();
        ValueTask<AccountToken> PostAccountLoginAsync(LogInWithPasswordRequest request);
        ValueTask<AccountToken> PostAccountRegisterAsync(RegisterWithPasswordRequest request);
    }
}
