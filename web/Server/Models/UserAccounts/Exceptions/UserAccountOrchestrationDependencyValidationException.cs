namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class UserAccountOrchestrationDependencyValidationException : Exception
    {
        public UserAccountOrchestrationDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
