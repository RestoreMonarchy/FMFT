using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Extensions.Exceptions
{
    public interface IExceptionWrapper
    {
        Exception WrapException(Exception exception);
    }
}
