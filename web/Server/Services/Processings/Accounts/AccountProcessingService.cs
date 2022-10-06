using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Arguments;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Shared.Enums;
using FMFT.Web.Server.Brokers.Loggings;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public partial class AccountProcessingService : IAccountProcessingService
    {
        private readonly IAccountService accountService;
        private readonly IUrlBroker urlBroker;
        private readonly ILoggingBroker loggingBroker;

        public AccountProcessingService(IAccountService accountService, IUrlBroker urlBroker, ILoggingBroker loggingBroker)
        {
            this.accountService = accountService;
            this.urlBroker = urlBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask AuthorizeAccountAsync()
            => TryCatch(async () =>
            {
                await RetrieveAccountAsync();
            });

        public ValueTask AuthorizeAccountByUserIdAsync(int authorizedUserId)
            => TryCatch(async () =>
            {
                Account account = await RetrieveAccountAsync();
                if (account.UserId != authorizedUserId)
                {
                    throw new NotAuthorizedAccountProcessingException();
                }
            });

        public ValueTask AuthorizeAccountByRoleAsync(params UserRole[] authorizedRoles)
            => TryCatch(async () =>
            {
                Account account = await RetrieveAccountAsync();
                if (!authorizedRoles.Contains(account.Role))
                {
                    throw new NotAuthorizedAccountProcessingException();
                }
            });

        public ValueTask AuthorizeAccountByUserIdOrRolesAsync(int authorizedUserId, params UserRole[] authorizedRoles)
            => TryCatch(async () =>
            {
                Account account = await RetrieveAccountAsync();
                if (authorizedUserId != account.UserId && !authorizedRoles.Contains(account.Role))
                {
                    throw new NotAuthorizedAccountProcessingException();
                }
            });

        public ValueTask<Account> RetrieveAccountAsync()
            => TryCatch(async () =>
            {
                return await accountService.RetrieveAccountAsync();
            });

        public ValueTask SignOutAccountAsync()
            => TryCatch(async () =>
            {
                await accountService.SignOutAccountAsync();
            });

        public ValueTask SignInAccountAsync(SignInAccountParams @params)
            => TryCatch(async () =>
            {
                await accountService.SignInAccountAsync(@params);
            });

        public ValueTask<ExternalLogin> RetrieveExternalLoginAsync()
            => TryCatch(async () =>
            {
                return await accountService.RetrieveExternalLoginAsync();
            });

        public ValueTask ChallengeExternalLoginAsync(ChallengeExternalLoginArguments arguments)
            => TryCatch(async () =>
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
            });
    }
}
