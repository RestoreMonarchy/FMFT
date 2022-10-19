namespace FMFT.Web.Server.Brokers.Validations
{
    public partial interface IValidationBroker
    {
        bool IsStringInvalid(string value, bool required, int maxLength, int minLength = 0);
        bool IsStringNullOrEmpty(string value);
        bool IsStringInvalid(string value, int maxLength, int minLength = 0);
    }
}
