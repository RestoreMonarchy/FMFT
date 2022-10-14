using FMFT.Web.Server.Models.Shows.Exceptions;

namespace FMFT.Web.Server.Services.Processings.Shows
{
    public partial class ShowProcessingService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is ShowValidationException or ShowDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            }
            if (exception is ShowServiceException or ShowDependencyException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyException(innerException);
            }

            return CreateAndLogServiceException(exception);
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new ShowProcessingValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new ShowProcessingDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new ShowProcessingDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }
        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new ShowProcessingServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }
    }
}
