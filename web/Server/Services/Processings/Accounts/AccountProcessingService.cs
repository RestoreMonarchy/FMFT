using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Accounts.Params;
using FMFT.Web.Server.Services.Foundations.Accounts;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public partial class AccountProcessingService : TheStandardService, IAccountProcessingService
    {
        private readonly IAccountService accountService;
        private readonly ILoggingBroker loggingBroker;

        public AccountProcessingService(IAccountService accountService, ILoggingBroker loggingBroker)
        {
            this.accountService = accountService;
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

        public ValueTask<Account> RetrieveAccountAsync()
            => TryCatch(async () =>
            {
                return await accountService.RetrieveAccountAsync();
            });

        public ValueTask<AccountToken> CreateTokenAsync(CreateTokenParams @params)
            => TryCatch(async () =>
            {
                return await accountService.CreateTokenAsync(@params);
            });
    }
}
