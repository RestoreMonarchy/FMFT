using System.Collections;
using Xeptions;

namespace FMFT.Web.Client.Models.Shows.Exceptions
{
    public class UpdateShowValidationException : Xeption
    {
        public UpdateShowValidationException(Exception innerException, IDictionary data)
            : base(innerException, data)
        {

        }
    }
}
