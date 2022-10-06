using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class AlreadyExistsUserEmailException : Xeption
    {
        public AlreadyExistsUserEmailException() 
            : base("ERR006: User with this email already exists")
        {

        }
    }
}
