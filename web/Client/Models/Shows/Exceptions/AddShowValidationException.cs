using System.Collections;
using Xeptions;

namespace FMFT.Web.Client.Models.Shows.Exceptions
{
    public class AddShowValidationException : Xeption
    {
        public AddShowValidationException(Exception innerException, IDictionary data)
            : base(innerException, data)
        {

        }
    }
}
