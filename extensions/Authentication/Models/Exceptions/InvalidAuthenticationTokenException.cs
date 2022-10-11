namespace FMFT.Extensions.Authentication.Models.Exceptions
{
    public class InvalidAuthenticationTokenException : Exception
    {
        public InvalidAuthenticationTokenException(string message)
            : base(message)
        {

        }
    }
}
