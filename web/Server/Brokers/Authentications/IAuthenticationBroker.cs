namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        bool IsAuthenticated { get; }
        bool IsNotAuthenticated { get; }
        int AuthenticatedUserId { get; }        

        ValueTask SignInAsync(Dictionary<string, object> claimsDictionary, bool isPersistent, string authenticationMethod);
        ValueTask SignOutAsync();
    }
}