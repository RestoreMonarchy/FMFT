using FMFT.Web.Server.Models.ResetPasswordRequests;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;
using FMFT.Web.Server.Models.ResetPasswordRequests.Requests;

namespace FMFT.Web.Server.Services.Orchestrations.ResetPasswordRequests
{
    public interface IResetPasswordRequestOrchestrationService
    {
        ValueTask<ResetPasswordRequest> CreateResetPasswordRequestAsync(CreateResetPasswordRequestRequest request);
        ValueTask<ResetPasswordRequest> GetResetPasswordRequestAsync(Guid secretKey);
        ValueTask ResetPasswordAsync(ResetPasswordParams @params);
    }
}