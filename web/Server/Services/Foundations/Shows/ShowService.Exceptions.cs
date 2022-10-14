using FMFT.Web.Server.Models.Shows.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public partial class ShowService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is NotFoundShowException 
                or AddShowValidationException
                or UpdateShowValidationException
                or AuditoriumNotExistsShowException)
            {
                return CreateAndLogValidationException(exception);
            }

            return CreateAndLogServiceException(exception);
        }
        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new ShowValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new ShowDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new ShowDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }
        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new ShowServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }

    }
}
