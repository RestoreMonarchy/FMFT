using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;

namespace FMFT.Web.Client.Services.Views.Accounts
{
    public interface IAccountViewService
    {
        UserAccount Account { get; }

        ValueTask ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
        void ForceLoadNavigateTo(string url);
        ValueTask LoginAsync(LogInWithPasswordRequest request);
        ValueTask RegisterAsync(RegisterWithPasswordRequest request);
    }
}