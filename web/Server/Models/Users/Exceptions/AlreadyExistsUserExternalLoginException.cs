using FMFT.Extensions.Exceptions.Attributes;
using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithDependencyValidationException]
    public class AlreadyExistsUserExternalLoginException : Xeption
    {
        public AlreadyExistsUserExternalLoginException() 
            : base("ERR007: User with this external login already exists")
        {

        }
    }
}
