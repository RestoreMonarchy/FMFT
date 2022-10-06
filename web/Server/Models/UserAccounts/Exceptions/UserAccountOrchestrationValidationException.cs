namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class UserAccountOrchestrationValidationException : Exception
    {
        public UserAccountOrchestrationValidationException(Exception innerException) 
            : base(null, innerException)
        {

        }
    }
}
