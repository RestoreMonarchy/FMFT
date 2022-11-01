using FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;
using FMFT.Web.Server.Models.ResetPasswordRequests.Requests;
using FMFT.Web.Server.Services.Orchestrations.ResetPasswordRequests;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResetPasswordRequestsController : RESTFulController
    {
        private readonly IResetPasswordRequestOrchestrationService resetPasswordRequestService;

        public ResetPasswordRequestsController(IResetPasswordRequestOrchestrationService resetPasswordRequestService)
        {
            this.resetPasswordRequestService = resetPasswordRequestService;
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> RequestResetPassword([FromBody] CreateResetPasswordRequestRequest request)
        {
            try
            {
                await resetPasswordRequestService.CreateResetPasswordRequestAsync(request);

                return Ok();
            }
            catch (UserNotFoundResetPasswordRequestException exception)
            {
                return NotFound(exception);
            }
            catch (LimitReachedResetPasswordRequestException exception)
            {
                return Conflict(exception);
            }
            catch (NoPasswordUserResetPasswordRequestException exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost("reset")]
        public async ValueTask<IActionResult> ResetPassword([FromBody] ResetPasswordParams @params)
        {
            try
            {
                await resetPasswordRequestService.ResetPasswordAsync(@params);

                return Ok();
            }
            catch (ResetPasswordValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (NotFoundResetPasswordRequestException exception)
            {
                return NotFound(exception);
            }
            catch (AlreadyUsedResetPasswordRequestException exception)
            {
                return Conflict(exception);
            }
            catch (ExpiredResetPasswordRequestException exception)
            {
                return Gone(exception);
            }
        }
    }
}
