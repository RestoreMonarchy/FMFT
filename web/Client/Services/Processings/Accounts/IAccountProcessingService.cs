using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;

namespace FMFT.Web.Client.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        Account Account { get; }
        bool IsAuthenticated { get; }

        event Action OnAccountChanged;

        ValueTask ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
        ValueTask LoginAsync(LogInWithPasswordRequest request);
        ValueTask RegisterAsync(RegisterWithPasswordRequest request);
    }
}