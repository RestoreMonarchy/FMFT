namespace FMFT.Web.Server.Models.Shows.Exceptions
{
    public class ShowOrchestrationValidationException : Exception
    {
        public ShowOrchestrationValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowOrchestrationDependencyValidationException : Exception
    {
        public ShowOrchestrationDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowOrchestrationDependencyException : Exception
    {
        public ShowOrchestrationDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }

    public class ShowOrchestrationServiceException : Exception
    {
        public ShowOrchestrationServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
