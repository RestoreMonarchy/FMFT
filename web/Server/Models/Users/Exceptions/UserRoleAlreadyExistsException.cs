using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserRoleAlreadyExistsException : Xeption
    {
        public UserRoleAlreadyExistsException()
            : base("ERR010: User already has this role set")
        {

        }
    }
}
