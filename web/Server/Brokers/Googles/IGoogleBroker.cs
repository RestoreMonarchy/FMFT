using Google.Apis.Auth;

namespace FMFT.Web.Server.Brokers.Googles
{
    public interface IGoogleBroker
    {
        ValueTask<GoogleJsonWebSignature.Payload> ValidateCredentialsAsync(string credentials);
    }
}
