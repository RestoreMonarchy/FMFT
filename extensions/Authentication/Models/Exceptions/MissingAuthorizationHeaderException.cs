namespace FMFT.Extensions.Authentication.Models.Exceptions
{
    public class MissingAuthorizationHeaderException : Exception
    {
        public MissingAuthorizationHeaderException()
            : base("Missing authorization header")
        {

        }
    }
}
