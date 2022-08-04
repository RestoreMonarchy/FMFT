using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Info()
        {
            try
            {
                Account account = userAccountService.RetrieveAccount();
                return Ok(account);
            } catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterWithPasswordRequest request)
        {
            try
            {
                Account account = await userAccountService.RegisterWithPasswordAsync(request);
                return Ok(account);
            } catch (RegisterUserWithPasswordValidationException exception)
            {
                return BadRequest(exception);
            } catch (UserEmailAlreadyExistsException)
            {
                return Conflict();
            }
        } 

        [HttpPost("login")]
        public async ValueTask<IActionResult> LogIn([FromBody] SignInWithPasswordRequest request)
        {
            try
            {
                Account account = await userAccountService.SignInWithPasswordAsync(request);
                return Ok(account);
            } catch (UserPasswordNotMatchException exception)
            {
                return Forbidden(exception);
            }
        }

        [HttpGet("logout")]
        public async ValueTask<IActionResult> LogOut([FromQuery] string returnUrl = "/")
        {
            await userAccountService.SignOutAsync();
            return LocalRedirect(returnUrl);
        }

        [HttpPost("ExternalLogin")]
        public async ValueTask ExternalLogin([FromForm] string provider, [FromForm] string returnUrl = "/")
        {
            await userAccountService.ChallengeExternalLoginAsync(provider, returnUrl);
        }

        [HttpGet("ExternalLoginCallback")]
        public async ValueTask<IActionResult> ExternalLoginCallback()
        {
            try
            {
                await userAccountService.HandleExternalLoginCallbackAsync();
                return Redirect("/");
            } catch (RegisterUserWithLoginValidationException)
            {
                return Redirect("/Account/ExternalLoginConfirmation");
            } catch (ExternalLoginNotFoundException)
            {
                return Redirect("/Account/Login");
            } catch (UserEmailAlreadyExistsException)
            {
                return Redirect("/Account/Login");
            } catch (UserLoginAlreadyExistsException)
            {
                return Redirect("/Account/Login");
            }
        }

        [HttpPost("ExternalLoginConfirmation")]
        public async ValueTask<IActionResult> ExternalLoginConfirmation([FromBody] ConfirmExternalLoginRequest request)
        {
            try
            {
                Account account = await userAccountService.ConfirmExternalLoginAsync(request);
                return Ok(account);
            } catch (UserEmailAlreadyExistsException)
            {
                return Conflict();
            } catch (UserLoginAlreadyExistsException)
            {
                return Conflict();
            } catch (ExternalLoginNotFoundException)
            {
                return Unauthorized();
            } catch (RegisterUserWithLoginValidationException e)
            {
                return BadRequest(e);
            }            
        }
    }
}
