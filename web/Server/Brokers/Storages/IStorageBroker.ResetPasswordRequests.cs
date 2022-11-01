using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.ResetPasswordRequests;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<StoredProcedureResult> ExecuteResetPasswordAsync(ResetPasswordParams @params);
        ValueTask<ResetPasswordRequest> GetResetPasswordRequestAsync(Guid secretKey);
        ValueTask<StoredProcedureResult<ResetPasswordRequest>> CreateResetPasswordRequestAsync(CreateResetPasswordRequestParams @params);
    }
}
