using FMFT.Web.Server.Models.Reservations.Exceptions;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public partial class ReservationOrchestrationService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is ReservationProcessingValidationException or ReservationProcessingDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            }
            if (exception is ReservationProcessingServiceException or ReservationProcessingDependencyException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyException(innerException);
            }

            return CreateAndLogServiceException(exception);
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new ReservationOrchestrationValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new ReservationOrchestrationDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new ReservationOrchestrationDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }
        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new ReservationOrchestrationServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }
    }
}
