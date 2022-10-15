using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Client.Models.AccountTokens;
using RESTFulSense.WebAssembly.Exceptions;

namespace FMFT.Web.Client.Services.Foundations.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAPIBroker apiBroker;

        public AccountService(IAPIBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public async ValueTask<UserAccount> RetrieveAccountAsync()
        {
            try
            {
                UserAccount account = await apiBroker.GetUserAccountAsync();

                return account;
            } catch (HttpResponseUnauthorizedException)
            {
                throw new AccountNotAuthenticatedException();
            }
        }

        public async ValueTask<AccountToken> LoginAsync(LogInWithPasswordRequest request)
        {
            try
            {
                AccountToken accountToken = await apiBroker.PostAccountLoginAsync(request);

                return accountToken;
            } catch (HttpResponseForbiddenException)
            {
                throw new AccountPasswordNotMatchException();
            }
        }

        public async ValueTask<AccountToken> RegisterAsync(RegisterWithPasswordRequest request)
        {
            try
            {
                AccountToken accountToken = await apiBroker.PostAccountRegisterAsync(request);

                return accountToken;
            }
            catch (HttpResponseBadRequestException exception)
            {
                throw new AccountRegisterWithPasswordValidationException(exception, exception.Data);
            } catch (HttpResponseConflictException)
            {
                throw new AccountEmailAlreadyExistsException();
            }
        }
    }
}
