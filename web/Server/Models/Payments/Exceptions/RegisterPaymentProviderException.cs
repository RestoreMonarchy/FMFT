namespace FMFT.Web.Server.Models.Payments.Exceptions
{
    public class RegisterPaymentProviderException : Exception
    {
        public RegisterPaymentProviderException() 
            : base("ERR057: Failed to register payment at payment provider")
        {
            
        }
    }
}
