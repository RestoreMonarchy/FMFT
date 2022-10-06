namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserProcessingDependencyValidationException : Exception
    {
        public UserProcessingDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
