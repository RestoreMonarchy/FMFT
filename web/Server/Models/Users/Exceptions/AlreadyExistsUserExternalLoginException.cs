using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class AlreadyExistsUserExternalLoginException : Exception
    {
        public AlreadyExistsUserExternalLoginException() 
            : base("ERR007: User with this external login already exists")
        {

        }
    }
}
