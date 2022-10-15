namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class ReservationProcessingValidationException : Exception
    {
        public ReservationProcessingValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationProcessingDependencyValidationException : Exception
    {
        public ReservationProcessingDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationProcessingDependencyException : Exception
    {
        public ReservationProcessingDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationProcessingServiceException : Exception
    {
        public ReservationProcessingServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
