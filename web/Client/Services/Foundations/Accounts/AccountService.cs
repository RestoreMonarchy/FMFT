using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Exceptions;
using FMFT.Web.Client.Models.Accounts.Requests;
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

        public async ValueTask<Account> RetrieveAccountAsync()
        {
            try
            {
                Account account = await apiBroker.GetAccountInfoAsync();
                return account;
            } catch (HttpResponseUnauthorizedException)
            {
                throw new AccountNotAuthenticatedException();
            }
        }

        public async ValueTask<Account> LoginAsync(LogInWithPasswordRequest request)
        {
            try
            {
                Account account = await apiBroker.PostAccountLoginAsync(request);
                return account;
            } catch (HttpResponseForbiddenException)
            {
                throw new AccountPasswordNotMatchException();
            }
        }

        public async ValueTask<Account> RegisterAsync(RegisterWithPasswordRequest request)
        {
            try
            {
                Account account = await apiBroker.PostAccountRegisterAsync(request);
                return account;
            }
            catch (HttpResponseBadRequestException exception)
            {
                throw new AccountRegisterWithPasswordValidationException(exception, exception.Data);
            } catch (HttpResponseConflictException)
            {
                throw new AccountEmailAlreadyExistsException();
            }
        }

        public async ValueTask<Account> ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request)
        {
            try
            {
                Account account = await apiBroker.PostConfirmExternalLoginAsync(request);
                return account;
            } catch (HttpResponseConflictException)
            {
                throw new AccountEmailAlreadyExistsException();
            } catch (HttpResponseUnauthorizedException)
            {
                throw new AccountExternalLoginNotFoundException();
            } catch (HttpResponseBadRequestException exception)
            {
                throw new AccountRegisterWithLoginValidationException(exception, exception.Data);
            }
        }
    }
}
