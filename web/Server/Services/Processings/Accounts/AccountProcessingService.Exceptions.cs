using FMFT.Extensions.Authentication.Models.Exceptions;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Users.Exceptions;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public partial class AccountProcessingService
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
            if (exception is NotAuthorizedAccountProcessingException)
            {
                return CreateAndLogValidationException(exception);
            }
            if (exception is AccountValidationException || exception is AccountDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            }
            if (exception is AccountServiceException || exception is AccountDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyException(innerException);
            }

            return CreateAndLogServiceException(exception);
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception userValidationException = new AccountProcessingValidationException(exception);
            loggingBroker.LogError(userValidationException);

            return userValidationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception userDependencyException = new AccountProcessingDependencyException(exception);
            loggingBroker.LogError(userDependencyException);

            return userDependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception userDependencyValidationException = new AccountProcessingDependencyValidationException(exception);
            loggingBroker.LogError(userDependencyValidationException);

            return userDependencyValidationException;
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception userServiceException = new AccountProcessingServiceException(exception);
            loggingBroker.LogError(userServiceException);

            return userServiceException;
        }
    }
}
