using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Arguments;
using FMFT.Web.Shared.Models.Accounts.Exceptions;
using FMFT.Web.Shared.Models.Accounts.Params;
using FMFT.Web.Shared.Models.Shared.Enums;

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
