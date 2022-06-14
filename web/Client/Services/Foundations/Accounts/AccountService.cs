using FMFT.Web.Client.Brokers.APIs;
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

        public async ValueTask LoginAsync(SignInUserWithPasswordModel model)
        {
            try
            {
                await apiBroker.PostAccountLoginAsync(model);
            } catch (HttpResponseForbiddenException)
            {
                throw new UserPasswordNotMatchException();
            }
        }

        public async ValueTask RegisterAsync(RegisterUserWithPasswordModel model)
        {
            try
            {
                await apiBroker.PostAccountRegisterAsync(model);
            }
            catch (HttpResponseBadRequestException exception)
            {
                throw new RegisterUserWithPasswordValidationException(exception, exception.Data);
            } catch (HttpResponseConflictException)
            {
                throw new UserEmailAlreadyExistsException();
            }
        }
    }
}
