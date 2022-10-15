using FMFT.Web.Server.Models.Reservations.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Reservations
{
    public partial class ReservationService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is NotFoundReservationException
                or SeatAlreadyReservedReservationException
                or UserAlreadyReservedReservationException)
            {
                return CreateAndLogValidationException(exception);
            }

            return CreateAndLogServiceException(exception);
        }
        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new ReservationValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new ReservationDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new ReservationDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }
        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new ReservationServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }

    }
}
