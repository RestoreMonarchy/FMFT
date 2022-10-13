using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Models.AccountTokens;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<UserAccount> GetUserAccountAsync();
        ValueTask<string> PostAccountLoginAsync(LogInWithPasswordRequest request);
        ValueTask<string> PostAccountRegisterAsync(RegisterWithPasswordRequest request);
    }
}
