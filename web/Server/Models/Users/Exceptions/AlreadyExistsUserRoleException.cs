using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class AlreadyExistsUserRoleException : Exception
    {
        public AlreadyExistsUserRoleException()
            : base("ERR010: User already has this role set")
        {

        }
    }
}
