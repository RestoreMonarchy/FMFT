namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserProcessingServiceException : Exception
    {
        public UserProcessingServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
