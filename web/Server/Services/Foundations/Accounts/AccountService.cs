using FMFT.Extensions.Authentication.Models.Exceptions;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public partial class AccountService : IAccountService
    {
        private readonly IAuthenticationBroker authenticationBroker;
        private readonly ILoggingBroker loggingBroker;

        public AccountService(IAuthenticationBroker authenticationBroker, ILoggingBroker loggingBroker)
        {
            this.authenticationBroker = authenticationBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask AuthorizeAccountAsync()
        {
            await RetrieveAccountAsync();
        }

        public async ValueTask AuthorizeAccountByUserIdAsync(int authorizedUserId)
        {
            Account account = await RetrieveAccountAsync();
            if (account.UserId != authorizedUserId)
            {
                throw new NotAuthorizedAccountException();
            }
        }

        public ValueTask<Account> RetrieveAccountAsync()
        {
            try
            {
                Account account = authenticationBroker.GetTokenPayload<Account>();

                return ValueTask.FromResult(account);
            } catch (Exception exception) 
                when (exception is MissingAuthorizationHeaderException or InvalidAuthenticationTokenException)
            {
                loggingBroker.LogDebugError(exception);
                throw new NotAuthenticatedAccountException();
            }
        }

        public ValueTask<AccountToken> CreateTokenAsync(CreateTokenParams @params)
        {
            string token = authenticationBroker.CreateToken(@params.Account);
            AccountToken accountToken = new(token);

            return ValueTask.FromResult(accountToken);
        }
    }
}
