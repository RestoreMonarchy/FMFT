using FMFT.Web.Server.Brokers.Storages;
using FMFT.Web.Server.Models.Database;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;
using FMFT.Web.Server.Models.ResetPasswordRequests;
using FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions;
using FMFT.Web.Server.Brokers.Validations;
using FMFT.Web.Server.Brokers.Encryptions;

namespace FMFT.Web.Server.Services.Foundations.ResetPasswordRequests
{
    public partial class ResetPasswordRequestService : IResetPasswordRequestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;
        private readonly IEncryptionBroker encryptionBroker;

        public ResetPasswordRequestService(
            IStorageBroker storageBroker, 
            IValidationBroker validationBroker, 
            IEncryptionBroker encryptionBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
            this.encryptionBroker = encryptionBroker;
        }

        public async ValueTask<ResetPasswordRequest> CreateResetPasswordRequestAsync(CreateResetPasswordRequestParams @params)
        {
            StoredProcedureResult<ResetPasswordRequest> result = await storageBroker.CreateResetPasswordRequestAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new UserNotFoundResetPasswordRequestException();
            }
            if (result.ReturnValue == 2)
            {
                throw new LimitReachedResetPasswordRequestException();
            }
            if (result.ReturnValue == 3)
            {
                throw new NoPasswordUserResetPasswordRequestException();
            }
            return result.Result;
        }

        public async ValueTask<ResetPasswordRequest> GetResetPasswordRequestAsync(Guid secretKey)
        {
            ResetPasswordRequest resetPasswordRequest = await storageBroker.GetResetPasswordRequestAsync(secretKey);
            if (resetPasswordRequest == null)
            {
                throw new NotFoundResetPasswordRequestException();
            }

            return resetPasswordRequest;
        }

        public async ValueTask ResetPasswordAsync(ResetPasswordParams @params)
        {
            ValidateResetPasswordParams(@params);

            @params.NewPassword = encryptionBroker.HashPassword(@params.NewPassword);

            StoredProcedureResult result = await storageBroker.ExecuteResetPasswordAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new NotFoundResetPasswordRequestException();
            }
            if (result.ReturnValue == 2)
            {
                throw new AlreadyUsedResetPasswordRequestException();
            }
            if (result.ReturnValue == 3)
            {
                throw new ExpiredResetPasswordRequestException();
            }
        }
    }
}
