namespace FMFT.Web.Server.Brokers.Encryptions
{
    public interface IEncryptionBroker
    {
        string HashPassword(string passwordText);
        bool VerifyPassword(string passwordText, string passwordHash);
    }
}
