namespace FMFT.Web.Server.Brokers.Validations
{
    public partial class ValidationBroker
    {
        public bool IsStringInvalid(string value, bool required, int maxLength, int minLength = 0)
        {
            return !IsStringValid(value, required, maxLength, minLength);
        }

        public bool IsStringValid(string value, bool required, int maxLength, int minLength = 0)
        {
            if (!required)
                return true;

            if (string.IsNullOrEmpty(value))
                return false;

            if (value.Length < minLength)
                return false;

            if (value.Length > maxLength)
                return false;

            return true;
        }
    }
}
