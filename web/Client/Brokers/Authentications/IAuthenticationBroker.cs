namespace FMFT.Web.Client.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        void SignIn(IDictionary<string, object> claimsDictionary);
        void SignOut();
    }
}
