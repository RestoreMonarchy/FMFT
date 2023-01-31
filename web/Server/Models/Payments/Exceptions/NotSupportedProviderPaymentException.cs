namespace FMFT.Web.Server.Models.Payments.Exceptions
{
    public class NotSupportedProviderPaymentException : Exception
    {
        public NotSupportedProviderPaymentException()
            : base("ERR047: Payment provider is not supported")
        {

        }
    }
}
