namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class NotAuthorizedUserAccountOrchestrationException : Exception
    {
        public NotAuthorizedUserAccountOrchestrationException() 
            : base("ERR002: Account not authorized")
        {

        }
    }
}
