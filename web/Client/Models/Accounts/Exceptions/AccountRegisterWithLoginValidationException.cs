using System.Collections;
using Xeptions;

namespace FMFT.Web.Client.Models.Accounts.Exceptions
{
    public class AccountRegisterWithLoginValidationException : Xeption
    {
        public AccountRegisterWithLoginValidationException(Exception innerException, IDictionary data)
            : base(innerException, data)
        {

        }
    }
}
