namespace FMFT.Web.Server.Models.Payments.Exceptions
{
    public class NotSupportedPaymentProviderException : Exception
    {
        public NotSupportedPaymentProviderException()
            : base("ERR047: Payment provider is not supported")
        {

        }
    }
}
