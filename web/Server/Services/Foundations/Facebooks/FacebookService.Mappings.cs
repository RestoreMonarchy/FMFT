using FMFT.Web.Server.Models.Facebooks;
using Newtonsoft.Json.Linq;

namespace FMFT.Web.Server.Services.Foundations.Facebooks
{
    public partial class FacebookService
    {
        public FacebookUser MapJObjectToFacebookUser(JObject jObject)
        {
            return new()
            {
                Id = jObject.GetValue("id").ToString(),
                Name = jObject.GetValue("name").ToString(),
                FirstName = jObject.GetValue("first_name").ToString(),
                LastName = jObject.GetValue("last_name").ToString(),
                Email = jObject.GetValue("email")?.ToString() ?? null,
            };
        }
    }
}
