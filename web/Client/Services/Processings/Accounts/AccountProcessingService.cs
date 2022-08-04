using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Services.Foundations.Accounts;

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

        public async ValueTask UpdateAccountAsync()
        {
            try
            {
                Account = await accountService.RetrieveAccountAsync();
            } catch (AccountNotAuthenticatedException)
            {
                Account = null;
            }
            
            OnAccountChanged.Invoke();
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
