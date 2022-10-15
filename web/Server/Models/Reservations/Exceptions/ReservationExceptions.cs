namespace FMFT.Web.Server.Models.Reservations.Exceptions
{
    public class ReservationValidationException : Exception
    {
        public ReservationValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationDependencyValidationException : Exception
    {
        public ReservationDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationDependencyException : Exception
    {
        public ReservationDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ReservationServiceException : Exception
    {
        public ReservationServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
