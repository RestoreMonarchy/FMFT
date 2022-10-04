using FMFT.Extensions.Exceptions.Attributes;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace FMFT.Extensions.Exceptions
{
    public class ExceptionWrapper : IExceptionWrapper
    {
        public Func<Exception, Exception> ServiceExceptionFactory { get; set; }
        public Func<Exception, Exception> ServiceValidationExceptionFactory { get; set; }
        public Func<Exception, Exception> DependencyExceptionFactory { get; set; }
        public Func<Exception, Exception> DependencyValidationExceptionFactory { get; set; }

        public Exception WrapException(Exception exception)
        {
            Type exceptionType = exception.GetType();

            if (exceptionType.GetCustomAttribute<WrapWithServiceExceptionAttribute>() != null)
            {
                return ServiceExceptionFactory(exception);
            }
            if (exceptionType.GetCustomAttribute<WrapWithServiceValidationExceptionAttribute>() != null)
            {
                return ServiceValidationExceptionFactory(exception);
            }
            if (exceptionType.GetCustomAttribute<WrapWithDependencyExceptionAttribute>() != null)
            {
                return DependencyExceptionFactory(exception);
            }
            if (exceptionType.GetCustomAttribute<WrapWithDependencyValidationExceptionAttribute>() != null)
            {
                return DependencyValidationExceptionFactory(exception);
            }

            return exception;
        }
    }
}
