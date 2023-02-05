namespace FMFT.Web.Server.Models.Payments.Exceptions
{
    public class NotSupportedPaymentMethodException : Exception
    {
        public NotSupportedPaymentMethodException() 
            : base("ERR048: Payment method is not supported")
        {

        }
    }
}
