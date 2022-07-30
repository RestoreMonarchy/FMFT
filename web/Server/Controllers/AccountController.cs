using FMFT.Web.Server.Services.Processings.Accounts;
using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Shared.Models.Accounts.Exceptions;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : RESTFulController
    {
        private readonly IAccountProcessingService accountService;

        public AccountController(IAccountProcessingService accountService)
        {
            this.accountService = accountService;
        }

        [Authorize]
        [HttpGet("info")]
        public async ValueTask<IActionResult> Info()
        {
            UserInfo userInfo = await accountService.RetrieveAuthenticatedUserInfoAsync();
            return Ok(userInfo);
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterUserWithPasswordModel model)
        {
            try
            {
                UserInfo userInfo = await accountService.RegisterUserWithPasswordAsync(model);
                return Ok(userInfo);
            } catch (UserPasswordInvalidException exception)
            {
                return BadRequest(exception);
            } catch (RegisterUserWithPasswordValidationException exception)
            {
                return BadRequest(exception);
            } catch (UserEmailAlreadyExistsException)
            {
                return Conflict();
            }
        } 

        [HttpPost("login")]
        public async ValueTask<IActionResult> LogIn([FromBody] SignInUserWithPasswordModel model)
        {
            try
            {
                UserInfo userInfo = await accountService.SignInUserWithPasswordAsync(model);
                return Ok(userInfo);
            } catch (UserPasswordNotMatchException exception)
            {
                return Forbidden(exception);
            }
        }

        [HttpGet("logout")]
        public async ValueTask<IActionResult> LogOut([FromQuery] string returnUrl = "/")
        {
            await accountService.SignOutUserAsync();
            return LocalRedirect(returnUrl);
        }

        [HttpPost("ExternalLogin")]
        public async ValueTask ExternalLogin([FromForm] string provider, [FromForm] string returnUrl = "/")
        {
            await accountService.ChallengeExternalLoginAsync(provider, returnUrl);
        }

        [HttpGet("ExternalLoginCallback")]
        public async ValueTask<IActionResult> ExternalLoginCallback()
        {
            try
            {
                await accountService.HandleExternalLoginCallbackAsync();
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
        public async ValueTask<IActionResult> ExternalLoginConfirmation([FromBody] ExternalLoginConfirmationModel model)
        {
            try
            {
                UserInfo userInfo = await accountService.ConfirmExternalLoginAsync(model);
                return Ok(userInfo);
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
