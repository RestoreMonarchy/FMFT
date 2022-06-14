namespace FMFT.Web.Server.Brokers.Authentications
{
    public interface IAuthenticationBroker
    {
        bool IsAuthenticated { get; }
        int AuthenticatedUserId { get; }
        bool IsNotAuthenticated { get; }

        ValueTask SignInAsync(Dictionary<string, object> claimsDictionary, bool isPersistent, string authenticationMethod);
        ValueTask SignOutAsync();
    }
}