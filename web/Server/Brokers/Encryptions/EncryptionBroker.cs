namespace FMFT.Web.Server.Brokers.Encryptions
{
    public class EncryptionBroker : IEncryptionBroker
    {
        public string HashPassword(string passwordText)
        {
            return BCrypt.Net.BCrypt.HashPassword(passwordText);
        }

        public bool VerifyPassword(string passwordText, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(passwordText, passwordHash);
        }
    }
}
