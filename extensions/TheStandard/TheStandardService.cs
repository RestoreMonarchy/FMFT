using System;
using System.Threading.Tasks;

namespace FMFT.Extensions.TheStandard
{
    public abstract class TheStandardService
    {
        protected delegate ValueTask<T> ReturningDelegate<T>();
        protected delegate ValueTask ReturningDelegate();

        protected async ValueTask TryCatch(ReturningDelegate function)
        {
            try
            {
                await function();
            }
            catch (Exception exception)
            {
                Exception wrappedException = WrapException(exception);
                throw wrappedException;
            }
        }

        protected async ValueTask<T> TryCatch<T>(ReturningDelegate<T> function)
        {
            try
            {
                return await function();
            }
            catch (Exception exception)
            {
                Exception wrappedException = WrapException(exception);
                throw wrappedException;
            }
        }

        protected virtual Exception WrapException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
