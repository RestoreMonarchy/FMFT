using FMFT.Web.Server.Models.Seats.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Seats
{
    public partial class SeatService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is NotFoundSeatException)
            {
                return CreateAndLogValidationException(exception);
            }

            return CreateAndLogServiceException(exception);
        }
        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new SeatValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new SeatDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new SeatDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }
        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new SeatServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }
    }
}
