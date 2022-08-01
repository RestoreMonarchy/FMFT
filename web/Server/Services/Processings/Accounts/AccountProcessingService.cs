using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Arguments;
using FMFT.Web.Shared.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public partial class AccountProcessingService : IAccountProcessingService
    {
        private readonly IAccountService accountService;
        private readonly IUrlBroker urlBroker;

        public AccountProcessingService(IAccountService accountService, IUrlBroker urlBroker)
        {
            this.accountService = accountService;
            this.urlBroker = urlBroker;
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

        public async ValueTask ChallengeExternalLoginAsync(ChallengeExternalLoginArguments arguments)
        {
            ChallengeExternalLoginParams @params = new()
            {
                Provider = arguments.Provider,
                RedirectUrl = urlBroker.Action("ExternalLoginCallback", "Account", 
                new 
                { 
                    returnUrl = arguments.ReturnUrl 
                })
            };
            await accountService.ChallengeExternalLoginAsync(@params);
        }
    }
}
