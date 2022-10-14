using FMFT.Web.Server.Models.Auditoriums.Exceptions;
using FMFT.Web.Server.Models.Users.Exceptions;

namespace FMFT.Web.Server.Services.Foundations.Auditoriums
{
    public partial class AuditoriumService
    {
        protected override Exception WrapException(Exception exception)
        {
            if (exception is NotFoundAuditoriumException)
            {
                return CreateAndLogValidationException(exception);
            }

            return CreateAndLogServiceException(exception);
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            Exception validationException = new AuditoriumValidationException(exception);
            loggingBroker.LogError(validationException);

            return validationException;
        }

        private Exception CreateAndLogDependencyException(Exception exception)
        {
            Exception dependencyException = new AuditoriumDependencyException(exception);
            loggingBroker.LogCritical(dependencyException);

            return dependencyException;
        }

        private Exception CreateAndLogDependencyValidationException(Exception exception)
        {
            Exception dependencyValidationException = new AuditoriumDependencyValidationException(exception);
            loggingBroker.LogError(dependencyValidationException);

            return dependencyValidationException;
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            Exception serviceException = new AuditoriumServiceException(exception);
            loggingBroker.LogError(serviceException);

            return serviceException;
        }
    }
}
