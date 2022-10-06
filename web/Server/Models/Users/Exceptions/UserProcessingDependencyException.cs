namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserProcessingDependencyException : Exception
    {
        public UserProcessingDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
