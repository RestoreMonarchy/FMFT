namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        string CreateToken<T>(T payload);
        T GetTokenPayload<T>();
    }
}