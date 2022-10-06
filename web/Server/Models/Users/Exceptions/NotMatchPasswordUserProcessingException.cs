using FMFT.Extensions.Exceptions.Attributes;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithServiceValidationException]
    public class NotMatchPasswordUserProcessingException : Exception
    {
        public NotMatchPasswordUserProcessingException()
            : base("ERR009: Invalid user credentials")
        {

        }
    }
}
