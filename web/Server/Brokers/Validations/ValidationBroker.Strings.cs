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
            if (string.IsNullOrEmpty(value) && required)
                return false;

            int length = value?.Length ?? 0;

            if (length < minLength)
                return false;

            if (length > maxLength)
                return false;

            return true;
        }
    }
}
