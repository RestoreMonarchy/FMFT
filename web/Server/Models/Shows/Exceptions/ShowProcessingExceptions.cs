namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class ShowProcessingValidationException : Exception
    {
        public ShowProcessingValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowProcessingDependencyValidationException : Exception
    {
        public ShowProcessingDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowProcessingDependencyException : Exception
    {
        public ShowProcessingDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowProcessingServiceException : Exception
    {
        public ShowProcessingServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
