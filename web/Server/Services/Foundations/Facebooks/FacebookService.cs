using FMFT.Web.Server.Brokers.Facebooks;
using FMFT.Web.Server.Models.Facebooks;
using Newtonsoft.Json.Linq;

namespace FMFT.Web.Server.Services.Foundations.Facebooks
{
    public partial class FacebookService : IFacebookService
    {
        private readonly IFacebookBroker facebookBroker;

        public FacebookService(IFacebookBroker facebookBroker)
        {
            this.facebookBroker = facebookBroker;
        }

        public async ValueTask<FacebookUser> GetFacebookUserAsync(string accessToken)
        {
            JObject jObject = await facebookBroker.GetUserProfileAsync(accessToken);
            FacebookUser user = MapJObjectToFacebookUser(jObject);

            return user;
        }
    }
}
