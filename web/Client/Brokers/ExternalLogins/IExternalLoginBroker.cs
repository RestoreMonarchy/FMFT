namespace FMFT.Web.Client.Brokers.ExternalLogins
{
    public interface IExternalLoginBroker
    {
        ValueTask InitializeFacebookAsync();
        ValueTask LoginFacebookAsync();
    }
}