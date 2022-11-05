using Newtonsoft.Json.Linq;

namespace FMFT.Web.Server.Brokers.Facebooks
{
    public interface IFacebookBroker
    {
        ValueTask<JObject> GetUserProfileAsync(string accessToken);
    }
}