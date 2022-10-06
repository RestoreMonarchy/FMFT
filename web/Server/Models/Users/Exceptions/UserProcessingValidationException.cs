namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserProcessingValidationException : Exception
    {
        public UserProcessingValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
