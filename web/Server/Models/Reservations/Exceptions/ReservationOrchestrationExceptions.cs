namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class ReservationOrchestrationValidationException : Exception
    {
        public ReservationOrchestrationValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationOrchestrationDependencyValidationException : Exception
    {
        public ReservationOrchestrationDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationOrchestrationDependencyException : Exception
    {
        public ReservationOrchestrationDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationOrchestrationServiceException : Exception
    {
        public ReservationOrchestrationServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
