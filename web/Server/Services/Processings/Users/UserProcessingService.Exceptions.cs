using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Users.Exceptions;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService
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
            if (exception is UserValidationException || exception is UserDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            } 
            else if (exception is UserServiceException || exception is UserDependencyException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyException(innerException);
            } 
            else if (exception is NotMatchPasswordUserProcessingException)
            {
                return CreateAndLogValidationException(exception);
            } 
            else
            {
                return CreateAndLogServiceException(exception);
            }
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception userValidationException = new UserProcessingValidationException(exception);
            loggingBroker.LogError(userValidationException);

            return userValidationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception userDependencyException = new UserProcessingDependencyException(exception);
            loggingBroker.LogError(userDependencyException);

            return userDependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception userDependencyValidationException = new UserProcessingDependencyValidationException(exception);
            loggingBroker.LogError(userDependencyValidationException);

            return userDependencyValidationException;
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception userServiceException = new UserProcessingServiceException(exception);
            loggingBroker.LogError(userServiceException);

            return userServiceException;
        }
    }
}
