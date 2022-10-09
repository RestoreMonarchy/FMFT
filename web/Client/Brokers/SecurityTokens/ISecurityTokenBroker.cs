namespace FMFT.Web.Client.Brokers.SecurityTokens
{
    public interface ISecurityTokenBroker
    {
        T DeserializeJWT<T>(string jwt);
    }
}