using FacebookCore;
using FMFT.Web.Server.Models.Options.Authentications;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace FMFT.Web.Server.Brokers.Facebooks
{
    public class FacebookBroker : IFacebookBroker
    {
        private readonly FacebookAuthenticationOptions options;

        private readonly FacebookClient client;

        public FacebookBroker(IOptions<FacebookAuthenticationOptions> options)
        {
            this.options = options.Value;

            client = GetClient();
        }

        private FacebookClient GetClient()
        {
            return new FacebookClient(options.AppId, options.AppSecret);
        }

        public async ValueTask<JObject> GetUserProfileAsync(string accessToken)
        {
            string[] fields = new string[]
            {
                "id",
                "name",
                "first_name",
                "last_name",
                "picture",
                "email"
            };

            return await client.GetUserApi(accessToken).RequestInformationAsync(fields);
        }
    }
}
