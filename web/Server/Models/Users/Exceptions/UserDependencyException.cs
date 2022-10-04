namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserDependencyException : Exception
    {
        public UserDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
