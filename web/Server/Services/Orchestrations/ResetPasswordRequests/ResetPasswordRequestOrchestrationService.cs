using FMFT.Web.Server.Models.Emails.Params;
using FMFT.Web.Server.Models.ResetPasswordRequests;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;
using FMFT.Web.Server.Models.ResetPasswordRequests.Requests;
using FMFT.Web.Server.Services.Foundations.Emails;
using FMFT.Web.Server.Services.Foundations.ResetPasswordRequests;

namespace FMFT.Web.Server.Services.Orchestrations.ResetPasswordRequests
{
    public class ResetPasswordRequestOrchestrationService : IResetPasswordRequestOrchestrationService
    {
        private readonly IResetPasswordRequestService resetPasswordRequestService;
        private readonly IEmailService emailService;

        public ResetPasswordRequestOrchestrationService(
            IResetPasswordRequestService resetPasswordRequestService, 
            IEmailService emailService)
        {
            this.resetPasswordRequestService = resetPasswordRequestService;
            this.emailService = emailService;
        }

        public async ValueTask<ResetPasswordRequest> CreateResetPasswordRequestAsync(CreateResetPasswordRequestRequest request)
        {
            TimeSpan expireTime = TimeSpan.FromMinutes(30);

            CreateResetPasswordRequestParams @createResetPasswordRequestParams = new()
            {
                Email = request.Email,
                ExpireDate = DateTime.Now.Add(expireTime)
            };

            ResetPasswordRequest resetPasswordRequest = 
                await resetPasswordRequestService.CreateResetPasswordRequestAsync(@createResetPasswordRequestParams);

            ResetPasswordEmailParams @resetPasswordEmailParams = new()
            {
                Email = resetPasswordRequest.User.Email,
                FirstName = resetPasswordRequest.User.FirstName,
                ExpireTime = expireTime,
                SecretKey = resetPasswordRequest.SecretKey
            };

            await emailService.SendResetPasswordEmailAsync(resetPasswordRequest.User.Email, @resetPasswordEmailParams);

            return resetPasswordRequest;
        }

        public async ValueTask<ResetPasswordRequest> GetResetPasswordRequestAsync(Guid secretKey)
        {
            return await resetPasswordRequestService.GetResetPasswordRequestAsync(secretKey);
        }

        public async ValueTask ResetPasswordAsync(ResetPasswordParams @params)
        {
            await resetPasswordRequestService.ResetPasswordAsync(@params);
        }
    }
}
