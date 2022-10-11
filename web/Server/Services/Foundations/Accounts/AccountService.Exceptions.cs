using FMFT.Extensions.Authentication.Models.Exceptions;
using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Models.Accounts.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public partial class AccountService
    {
        protected override Exception WrapException(Exception exception)
        {
            // Map dependency exceptions
            if (exception is MissingAuthorizationHeaderException or InvalidAuthenticationTokenException) 
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
