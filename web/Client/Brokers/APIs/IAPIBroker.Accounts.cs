using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Accounts;
using FMFT.Web.Client.Models.API.Accounts.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<UserAccount>> GetUserAccountAsync();
        ValueTask<APIResponse<AccountToken>> PostAccountLoginAsync(LogInWithPasswordRequest request);
        ValueTask<APIResponse<AccountToken>> PostAccountRegisterAsync(RegisterWithPasswordRequest request);
        ValueTask<APIResponse> PostAccountChangePasswordAsync(ChangePasswordRequest request);
        ValueTask<APIResponse<AccountToken>> PostAccountFacebookLoginAsync(FacebookLoginRequest request);
    }
}
