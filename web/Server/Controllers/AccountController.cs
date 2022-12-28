using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions;
using FMFT.Web.Server.Models.ResetPasswordRequests.Params;
using FMFT.Web.Server.Models.ResetPasswordRequests.Requests;
using FMFT.Web.Server.Models.UserAccounts;
using FMFT.Web.Server.Models.UserAccounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Requests;
using FMFT.Web.Server.Services.Orchestrations.ResetPasswordRequests;
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
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("user")]
        public async ValueTask<IActionResult> UserAccount()
        {
            try
            {
                UserAccount userAccount = await userAccountService.RetrieveUserAccountAsync();

                return Ok(userAccount);
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotFoundUserException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterUserWithPasswordRequest request)
        {
            try
            {
                AccountToken accountToken = await userAccountService.RegisterWithPasswordAsync(request);

                return Ok(accountToken);
            } catch (RegisterUserWithPasswordValidationException exception)
            {
                return BadRequest(exception);
            } catch (AlreadyExistsEmailUserException exception)
            {
                return Conflict(exception);
            }
        } 

        [HttpPost("login")]
        public async ValueTask<IActionResult> LogIn([FromBody] SignInWithPasswordRequest request)
        {
            try
            {
                AccountToken accountToken = await userAccountService.SignInWithPasswordAsync(request);
                
                return Ok(accountToken);
            } catch (NotMatchPasswordUserException exception)
            {
                return Forbidden(exception);
            } catch (NotFoundUserException)
            {
                // TODO: Handle it better
                NotMatchPasswordUserException credentialsException = new();

                return Forbidden(credentialsException);
            }
        }

        [HttpPost("changepassword")]
        public async ValueTask<IActionResult> ChangePassword([FromBody] ChangeUserAccountPasswordRequest request)
        {
            try
            {
                await userAccountService.ChangeUserAccountPasswordAsync(request);

                return Ok();
            } catch (NotMatchPasswordUserException exception)
            {
                return BadRequest(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpPost("login/facebook")]
        public async ValueTask<IActionResult> LoginFacebook([FromBody] LoginWithFacebookRequest request)
        {
            try
            {
                AccountToken accountToken = await userAccountService.LoginWithFacebookAsync(request);

                return Ok(accountToken);
            } catch (AlreadyExistsEmailUserException exception)
            {
                return Conflict(exception);
            } catch (AlreadyExistsUserExternalLoginException exception)
            {
                return Conflict(exception);
            } catch (RegisterUserWithLoginValidationException exception)
            {
                return BadRequest(exception);
            }            
        }

        [HttpPost("login/google")]
        public async ValueTask<IActionResult> LoginGoogle([FromBody] LoginWithGoogleRequest request)
        {
            try
            {
                AccountToken accountToken = await userAccountService.LoginWithGoogleAsync(request);

                return Ok(accountToken);
            }
            catch (AlreadyExistsEmailUserException exception)
            {
                return Conflict(exception);
            }
            catch (AlreadyExistsUserExternalLoginException exception)
            {
                return Conflict(exception);
            }
            catch (RegisterUserWithLoginValidationException exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
