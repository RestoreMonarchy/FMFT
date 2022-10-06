using FMFT.Web.Server.Models.Accounts.Exceptions;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public partial class AccountProcessingService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is NotAuthorizedAccountProcessingException)
            {
                return CreateAndLogValidationException(exception);
            }
            if (exception is AccountValidationException or AccountDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            }
            if (exception is AccountServiceException or AccountDependencyValidationException)
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
