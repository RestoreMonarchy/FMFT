using System.Net.Mail;

namespace FMFT.Web.Server.Brokers.Validations
{
    public partial class ValidationBroker
    {
        public bool IsEmailInvalid(string email)
        {
            return !IsEmailValid(email);
        }

        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                MailAddress addr = new(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
