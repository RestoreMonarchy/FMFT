namespace FMFT.Web.Server.Models.Seats.Exceptions
{
    public class SeatValidationException : Exception
    {
        public SeatValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class SeatDependencyValidationException : Exception
    {
        public SeatDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class SeatDependencyException : Exception
    {
        public SeatDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class SeatServiceException : Exception
    {
        public SeatServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
