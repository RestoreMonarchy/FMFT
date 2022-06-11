namespace FMFT.Web.Server.Brokers.Validations
{
    public partial interface IValidationBroker
    {
        bool IsStringInvalid(string value, bool required, int maxLength, int minLength = 0);
    }
}
