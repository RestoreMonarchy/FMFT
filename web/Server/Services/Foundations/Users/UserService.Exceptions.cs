using FMFT.Web.Server.Models.Users.Exceptions;
using System;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public partial class UserService
    {
        private delegate ValueTask<T> ReturningDelegate<T>();
        private delegate ValueTask ReturningDelegate();

        private async ValueTask TryCatch(ReturningDelegate function)
        {
            try
            {
                await function();
            } catch (Exception exception)
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
            } catch (Exception exception)
            {
                Exception wrappedException = WrapException(exception);
                throw wrappedException;
            }
        }

        private Exception WrapException(Exception exception)
        {
            Exception wrappedException = exceptionWrapper.WrapException(exception);
            if (wrappedException == exception)
            {
                wrappedException = CreateAndLogServiceException(exception);
            }

            return wrappedException;
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception userValidationException = new UserValidationException(exception);
            loggingBroker.LogError(userValidationException);

            return userValidationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception userDependencyException = new UserDependencyException(exception);
            loggingBroker.LogError(userDependencyException);

            return userDependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception userDependencyValidationException = new UserDependencyValidationException(exception);
            loggingBroker.LogError(userDependencyValidationException);

            return userDependencyValidationException;
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception userServiceException = new UserServiceException(exception);
            loggingBroker.LogError(userServiceException);

            return userServiceException;
        }
    }
}
