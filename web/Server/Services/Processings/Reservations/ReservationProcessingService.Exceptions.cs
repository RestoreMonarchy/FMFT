using FMFT.Web.Server.Models.Reservations.Exceptions;

namespace FMFT.Web.Server.Services.Processings.Reservations
{
    public partial class ReservationProcessingService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is ReservationValidationException or ReservationDependencyValidationException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyValidationException(innerException);
            }
            if (exception is ReservationServiceException or ReservationDependencyException)
            {
                Exception innerException = exception.InnerException;

                return CreateAndLogDependencyException(innerException);
            }

            return CreateAndLogServiceException(exception);
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new ReservationProcessingValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new ReservationProcessingDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new ReservationProcessingDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }
        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new ReservationProcessingServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }
    }
}
