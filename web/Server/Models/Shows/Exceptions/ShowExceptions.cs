namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class ShowValidationException : Exception
    {
        public ShowValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowDependencyValidationException : Exception
    {
        public ShowDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowDependencyException : Exception
    {
        public ShowDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowServiceException : Exception
    {
        public ShowServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
