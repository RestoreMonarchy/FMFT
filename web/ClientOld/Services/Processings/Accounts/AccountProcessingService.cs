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

        public async ValueTask<UserAccount> RetrieveAccountAsync()
        {
            return await accountService.RetrieveAccountAsync();
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
