using FMFT.Web.Client.Models.API.Users.Requests;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ResetPasswordRequests.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse> CreateResetPasswordRequestAsync(CreateResetPasswordRequestRequest request);
        ValueTask<APIResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
