using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public partial class AccountProcessingService : IAccountProcessingService
    {
        private readonly IAccountService accountService;

        public AccountProcessingService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public Account RetrieveAccount()
        {
            return accountService.RetrieveAccount();
        }

        public async ValueTask SignOutAccountAsync()
        {
            await accountService.SignOutAccountAsync();
        }

        public async ValueTask SignInAccountAsync(SignInAccountParams @params)
        {
            await accountService.SignInAccountAsync(@params);
        }

        public async ValueTask<ExternalLogin> RetrieveExternalLoginAsync()
        {
            return await accountService.RetrieveExternalLoginAsync();
        }
    }
}
