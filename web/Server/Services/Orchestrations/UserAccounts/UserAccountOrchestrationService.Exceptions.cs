using FMFT.Web.Server.Models.UserAccounts.Exceptions;
using FMFT.Web.Server.Models.Users.Exceptions;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public partial class UserAccountOrchestrationService
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
            if (exception is UserProcessingValidationException || exception is UserProcessingDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            }
            else if (exception is UserProcessingServiceException || exception is UserProcessingDependencyException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyException(innerException);
            }
            else
            {
                return CreateAndLogServiceException(exception);
            }
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception userValidationException = new UserAccountOrchestrationValidationException(exception);
            loggingBroker.LogError(userValidationException);

            return userValidationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception userDependencyException = new UserAccountOrchestrationDependencyException(exception);
            loggingBroker.LogError(userDependencyException);

            return userDependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception userDependencyValidationException = new UserAccountOrchestrationDependencyValidationException(exception);
            loggingBroker.LogError(userDependencyValidationException);

            return userDependencyValidationException;
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception userServiceException = new UserAccountOrchestrationServiceException(exception);
            loggingBroker.LogError(userServiceException);

            return userServiceException;
        }
    }
}
