namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserDependencyValidationException : Exception
    {
        public UserDependencyValidationException(Exception innerException) 
            : base(null, innerException)
        {

        }
    }
}
