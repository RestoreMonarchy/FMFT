using FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;

namespace FMFT.Web.Server.Services.Foundations.ResetPasswordRequests
{
    public partial class ResetPasswordRequestService
    {
        private void ValidateResetPasswordParams(ResetPasswordParams @params)
        {
            ResetPasswordValidationException validationException = new();

            if (validationBroker.IsPasswordInvalid(@params.NewPassword))
            {
                validationException.UpsertDataList("NewPassword", "The new password must have at least 8 characters");
            }

            validationException.ThrowIfContainsErrors();
        }
    }
}
