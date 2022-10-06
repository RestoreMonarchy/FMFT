namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class UserAccountOrchestrationDependencyException : Exception
    {
        public UserAccountOrchestrationDependencyException(Exception innerException)
            : base(null, innerException)
        {
            
        }
    }
}
