using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Brokers.Urls;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Server.Services.Foundations.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public partial class AccountProcessingService : TheStandardService, IAccountProcessingService
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

        public ValueTask<string> CreateTokenAsync(CreateTokenParams @params)
            => TryCatch(async () =>
            {
                return await accountService.CreateTokenAsync(@params);
            });
    }
}
