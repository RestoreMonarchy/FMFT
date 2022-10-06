using FMFT.Web.Server.Models.Users.Exceptions;
using System;

namespace FMFT.Web.Server.Services.Foundations.Users
{
    public partial class UserService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is NotFoundUserException 
                or RegisterUserWithLoginValidationException
                or RegisterUserWithPasswordValidationException)
            {
                return CreateAndLogValidationException(exception);
            }
            if (exception is AlreadyExistsUserCultureException 
                or AlreadyExistsEmailUserException 
                or AlreadyExistsUserExternalLoginException 
                or AlreadyExistsUserRoleException)
            {
                return CreateAndLogDependencyValidationException(exception);
            }

            return CreateAndLogServiceException(exception);
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
            loggingBroker.LogCritical(userDependencyException);

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
