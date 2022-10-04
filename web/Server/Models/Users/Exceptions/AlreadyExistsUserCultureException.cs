using FMFT.Extensions.Exceptions.Attributes;
using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithDependencyValidationException]
    public class AlreadyExistsUserCultureException : Xeption
    {
        public AlreadyExistsUserCultureException()
            : base("ERR011: User already has this culture set")
        {

        }
    }
}
