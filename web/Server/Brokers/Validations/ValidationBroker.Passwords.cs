namespace FMFT.Web.Server.Brokers.Validations
{
    public partial class ValidationBroker
    {
        public bool IsPasswordInvalid(string password)
        {
            return !IsPasswordValid(password);
        }

        public bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < 8)
                return false;

            return true;
        }
    }
}
