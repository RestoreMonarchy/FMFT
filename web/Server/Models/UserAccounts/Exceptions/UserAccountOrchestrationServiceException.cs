namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class UserAccountOrchestrationServiceException : Exception
    {
        public UserAccountOrchestrationServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
