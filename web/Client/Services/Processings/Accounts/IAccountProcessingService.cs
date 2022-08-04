using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        Account Account { get; }
        bool IsAuthenticated { get; }

        event Action OnAccountChanged;

        void AuthorizeAccountByRole(params UserRole[] authorizedRoles);
        void AuthorizeAccountByUserId(int authorizedUserId);
        void AuthorizeAccountByUserIdOrRoles(int authorizedUserId, params UserRole[] authorizedRoles);
        ValueTask ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
        ValueTask LoginAsync(LogInWithPasswordRequest request);
        ValueTask RegisterAsync(RegisterWithPasswordRequest request);
        Account RetrieveAccount();
        ValueTask UpdateAccountAsync();
    }
}