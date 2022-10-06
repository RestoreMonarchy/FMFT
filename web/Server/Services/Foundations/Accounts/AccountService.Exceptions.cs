using FMFT.Extensions.Authentication.Models.Exceptions;
using FMFT.Extensions.Exceptions;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Users.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public partial class AccountService
    {
        private delegate ValueTask<T> ReturningDelegate<T>();
        private delegate ValueTask ReturningDelegate();

        private async ValueTask TryCatch(ReturningDelegate function)
        {
            try
            {
                await function();
            }
            catch (Exception exception)
            {
                Exception wrappedException = WrapException(exception);
                throw wrappedException;
            }
        }

        private async ValueTask<T> TryCatch<T>(ReturningDelegate<T> function)
        {
            try
            {
                return await function();
            }
            catch (Exception exception)
            {
                Exception wrappedException = WrapException(exception);
                throw wrappedException;
            }
        }

        private Exception WrapException(Exception exception)
        {
            // Map dependency exceptions
            if (exception is ExternalNotAuthenticatedException)
            {
                Exception innerException = new NotFoundExternalLoginException();

                return CreateAndLogDependencyValidationException(innerException);
            }
            if (exception is NotAuthenticatedException) 
            {
                Exception innerException = new NotAuthenticatedAccountException();

                return CreateAndLogDependencyValidationException(innerException);
            }            

            return CreateAndLogServiceException(exception);
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception userValidationException = new AccountValidationException(exception);
            loggingBroker.LogError(userValidationException);

            return userValidationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception userDependencyException = new AccountDependencyException(exception);
            loggingBroker.LogError(userDependencyException);

            return userDependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception userDependencyValidationException = new AccountDependencyValidationException(exception);
            loggingBroker.LogError(userDependencyValidationException);

            return userDependencyValidationException;
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception userServiceException = new AccountServiceException(exception);
            loggingBroker.LogError(userServiceException);

            return userServiceException;
        }
    }
}
