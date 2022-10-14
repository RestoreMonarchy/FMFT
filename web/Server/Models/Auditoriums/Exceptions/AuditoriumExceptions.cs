namespace FMFT.Web.Server.Models.Auditoriums.Exceptions
{
    public class AuditoriumValidationException : Exception 
    {
        public AuditoriumValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class AuditoriumDependencyValidationException : Exception 
    {
        public AuditoriumDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class AuditoriumDependencyException : Exception
    {
        public AuditoriumDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class AuditoriumServiceException : Exception
    {
        public AuditoriumServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
