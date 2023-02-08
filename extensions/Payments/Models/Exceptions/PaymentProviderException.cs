namespace FMFT.Extensions.Payments.Models.Exceptions
{
    public class PaymentProviderException : Exception
    {
        public PaymentProviderException() 
            : base("ERR049: Error when trying to register payment at payment provider")
        {

        }
    }
}
