using Google.Apis.Auth;

namespace FMFT.Web.Server.Brokers.Googles
{
    public class GoogleBroker : IGoogleBroker
    {
        public async ValueTask<GoogleJsonWebSignature.Payload> ValidateCredentialsAsync(string credentials)
        {
            return await GoogleJsonWebSignature.ValidateAsync(credentials);            
        }
    }
}
