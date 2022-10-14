using FMFT.Web.Server.Models.Shows.Exceptions;

namespace FMFT.Web.Server.Services.Orchestrations.Shows
{
    public partial class ShowOrchestrationService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is ShowProcessingValidationException or ShowProcessingDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            }
            if (exception is ShowProcessingServiceException or ShowProcessingDependencyException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyException(innerException);
            }

            return CreateAndLogServiceException(exception);
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new ShowOrchestrationValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new ShowOrchestrationDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new ShowOrchestrationDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }
        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new ShowOrchestrationServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }
    }
}
