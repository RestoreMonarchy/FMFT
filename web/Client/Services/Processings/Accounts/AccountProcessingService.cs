using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Services.Foundations.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Processings.Accounts
{
    public class AccountProcessingService : IAccountProcessingService
    {
        private readonly IAccountService accountService;

        public AccountProcessingService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public Account Account { get; private set; }
        public bool IsAuthenticated => Account != null;
        public event Action OnAccountChanged;

        public Account RetrieveAccount() 
        { 
            if (Account == null)
            {
                throw new AccountNotAuthenticatedException();
            }
            return Account;
        }

        public async ValueTask UpdateAccountAsync()
        {
            try
            {
                Account = await accountService.RetrieveAccountAsync();
            } catch (AccountNotAuthenticatedException)
            {
                Account = null;
            }
            
            OnAccountChanged?.Invoke();
        }

        public void AuthorizeAccountByUserId(int authorizedUserId)
        {
            Account account = RetrieveAccount();
            if (account.UserId != authorizedUserId)
            {
                throw new AccountNotAuthorizedException();
            }
        }

        public void AuthorizeAccountByRole(params UserRole[] authorizedRoles)
        {
            Account account = RetrieveAccount();
            if (!authorizedRoles.Contains(account.Role))
            {
                throw new AccountNotAuthorizedException();
            }
        }

        public void AuthorizeAccountByUserIdOrRoles(int authorizedUserId, params UserRole[] authorizedRoles)
        {
            Account account = RetrieveAccount();
            if (authorizedUserId != account.UserId && !authorizedRoles.Contains(account.Role))
            {
                throw new AccountNotAuthorizedException();
            }
        }

        public async ValueTask ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request)
        {
            await accountService.ConfirmExternalLoginAsync(request);
        }

        public async ValueTask LoginAsync(LogInWithPasswordRequest request)
        {
            await accountService.LoginAsync(request);
        }

        public async ValueTask RegisterAsync(RegisterWithPasswordRequest request)
        {
            await accountService.RegisterAsync(request);
        }
    }
}
