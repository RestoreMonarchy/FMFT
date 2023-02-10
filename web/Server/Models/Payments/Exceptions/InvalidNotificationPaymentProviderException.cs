namespace FMFT.Web.Server.Models.Payments.Exceptions
{
    public class InvalidNotificationPaymentProviderException : Exception
    {
        public InvalidNotificationPaymentProviderException() 
            : base("ERR050: The payment provider notification could not be verified") 
        {

        }
    }
}
