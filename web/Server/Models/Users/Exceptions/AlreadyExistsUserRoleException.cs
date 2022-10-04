using FMFT.Extensions.Exceptions.Attributes;
using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithDependencyValidationException]
    public class AlreadyExistsUserRoleException : Xeption
    {
        public AlreadyExistsUserRoleException()
            : base("ERR010: User already has this role set")
        {

        }
    }
}
