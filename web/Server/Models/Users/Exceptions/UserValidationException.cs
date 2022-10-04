namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserValidationException : Exception
    {
        public UserValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
