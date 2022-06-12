using Xeptions;

namespace FMFT.Web.Shared.Models.Users.Exceptions
{
    public class UserPasswordInvalidException : Xeption
    {
        public UserPasswordInvalidException() 
            : base("Invalid user password")
        {
            UpsertDataList("Password", "Invalid");
        }
    }
}
