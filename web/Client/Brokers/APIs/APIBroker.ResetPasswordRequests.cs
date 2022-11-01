using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ResetPasswordRequests.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ResetPasswordRequestRelativeUrl = "api/resetpasswordrequests";

        public async ValueTask<APIResponse> CreateResetPasswordRequestAsync(CreateResetPasswordRequestRequest request)
        {
            string url = $"{ResetPasswordRequestRelativeUrl}/create";

            return await PostAsync(url, request);
        }

        public async ValueTask<APIResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            string url = $"{ResetPasswordRequestRelativeUrl}/reset";

            return await PostAsync(url, request);
        }
    }
}
