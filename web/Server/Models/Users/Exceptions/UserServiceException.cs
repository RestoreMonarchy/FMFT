namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserServiceException : Exception
    {
        public UserServiceException(Exception innerException) 
            : base(null, innerException)
        {

        }
    }
}
