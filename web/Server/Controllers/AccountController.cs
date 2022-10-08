using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : RESTFulController
    {
        private readonly IUserAccountOrchestrationService userAccountService;

        public AccountController(IUserAccountOrchestrationService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        [HttpGet("info")]
        public async ValueTask<IActionResult> Info()
        {
            try
            {
                Account account = await userAccountService.RetrieveAccountAsync();

                return Ok(account);
            } catch (UserAccountOrchestrationDependencyValidationException exception) 
                when (exception.InnerException is NotAuthenticatedAccountException)
            {
                Exception innerException = exception.InnerException;

                return Unauthorized(innerException);
            }
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterWithPasswordRequest request)
        {
            try
            {
                string token = await userAccountService.RegisterWithPasswordAsync(request);

                return Ok(token);
            } catch (UserAccountOrchestrationDependencyValidationException exception)
                when (exception.InnerException is RegisterUserWithPasswordValidationException)
            {
                Exception innerException = exception.InnerException;

                return BadRequest(innerException);
            } catch (UserAccountOrchestrationDependencyValidationException exception)
                when (exception.InnerException is AlreadyExistsEmailUserException)
            {
                Exception innerException = exception.InnerException;

                return Conflict(innerException);
            }
        } 

        [HttpPost("login")]
        public async ValueTask<IActionResult> LogIn([FromBody] SignInWithPasswordRequest request)
        {
            try
            {
                string token = await userAccountService.SignInWithPasswordAsync(request);
                
                return Ok(token);
            } catch (UserAccountOrchestrationDependencyValidationException exception) 
                when (exception.InnerException is NotMatchPasswordUserProcessingException)
            {
                Exception innerException = exception.InnerException;

                return Forbidden(innerException);
            } catch (UserAccountOrchestrationDependencyValidationException exception)
                when (exception.InnerException is NotFoundUserException)
            {
                Exception innerException = exception.InnerException;

                return Forbidden(innerException);
            }
        }
    }
}
