namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class NotAuthorizedUserAccountOrchestrationException : Exception
    {
        public NotAuthorizedUserAccountOrchestrationException() 
            : base("ERR021: User account not authorized")
        {

        }
    }
}
