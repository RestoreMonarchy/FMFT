using FMFT.Web.Server.Models.ResetPasswordRequests;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;

namespace FMFT.Web.Server.Services.Foundations.ResetPasswordRequests
{
    public interface IResetPasswordRequestService
    {
        ValueTask<ResetPasswordRequest> CreateResetPasswordRequestAsync(CreateResetPasswordRequestParams @params);
        ValueTask<ResetPasswordRequest> GetResetPasswordRequestAsync(Guid secretKey);
        ValueTask ResetPasswordAsync(ResetPasswordParams @params);
    }
}