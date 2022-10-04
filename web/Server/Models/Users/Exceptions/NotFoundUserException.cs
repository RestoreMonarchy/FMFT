using FMFT.Extensions.Exceptions.Attributes;
using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithServiceValidationException]
    public class NotFoundUserException : Xeption
    {
        public NotFoundUserException()
            : base("ERR008: User not found")
        {

        }
    }
}
