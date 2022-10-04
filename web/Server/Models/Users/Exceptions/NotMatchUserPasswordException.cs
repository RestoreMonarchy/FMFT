using FMFT.Extensions.Exceptions.Attributes;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithServiceValidationException]
    public class NotMatchUserPasswordException : Exception
    {
        public NotMatchUserPasswordException()
            : base("ERR009: Invalid user credentials")
        {

        }
    }
}
