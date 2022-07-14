using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
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

        public async ValueTask<UserInfo> RetrieveAccountInfoAsync()
        {
            try
            {
                UserInfo userInfo = await apiBroker.GetAccountInfoAsync();
                return userInfo;
            } catch (HttpResponseUnauthorizedException)
            {
                throw new UserNotAuthenticatedException();
            }
        }

        public async ValueTask<UserInfo> LoginAsync(SignInUserWithPasswordModel model)
        {
            try
            {
                UserInfo userInfo = await apiBroker.PostAccountLoginAsync(model);
                return userInfo;
            } catch (HttpResponseForbiddenException)
            {
                throw new UserPasswordNotMatchException();
            }
        }

        public async ValueTask<UserInfo> RegisterAsync(RegisterUserWithPasswordModel model)
        {
            try
            {
                UserInfo userInfo = await apiBroker.PostAccountRegisterAsync(model);
                return userInfo;
            }
            catch (HttpResponseBadRequestException exception)
            {
                throw new RegisterUserWithPasswordValidationException(exception, exception.Data);
            } catch (HttpResponseConflictException)
            {
                throw new UserEmailAlreadyExistsException();
            }
        }

        public async ValueTask<UserInfo> ConfirmExternalLoginAsync(ExternalLoginConfirmationModel model)
        {
            try
            {
                UserInfo userInfo = await apiBroker.PostExternalLoginConfirmationAsync(model);
                return userInfo;
            } catch (HttpResponseConflictException)
            {
                throw new UserEmailAlreadyExistsException();
            } catch (HttpResponseUnauthorizedException)
            {
                throw new ExternalLoginInfoNotFoundException();
            } catch (HttpResponseBadRequestException exception)
            {
                throw new RegisterUserWithLoginValidationException(exception, exception.Data);
            }
        }
    }
}
