using System.Collections;
using Xeptions;

namespace FMFT.Web.Client.Models.Accounts.Exceptions
{
    public class AccountRegisterWithPasswordValidationException : Xeption
    {
        public AccountRegisterWithPasswordValidationException(Exception innerException, IDictionary data) 
            : base(innerException, data)
        {

        }
    }
}
