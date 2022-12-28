using FMFT.Web.Server.Brokers.Googles;
using FMFT.Web.Server.Models.Googles;
using Google.Apis.Auth;

namespace FMFT.Web.Server.Services.Foundations.Googles
{
    public class GoogleService : IGoogleService
    {
        private readonly IGoogleBroker googleBroker;

        public GoogleService(IGoogleBroker googleBroker)
        {
            this.googleBroker = googleBroker;
        }

        public async ValueTask<GoogleUser> GetGoogleUserAsync(string credentials)
        {
            GoogleJsonWebSignature.Payload payload = await googleBroker.ValidateCredentialsAsync(credentials);

            return MapGoogleJsonWebSignaturePayloadToGoogleUser(payload);            
        }

        private GoogleUser MapGoogleJsonWebSignaturePayloadToGoogleUser(GoogleJsonWebSignature.Payload payload)
        {
            return new GoogleUser()
            {
                Id = payload.Email,
                Email = payload.Email,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                Name = payload.Name
            };
        }
    }
}
