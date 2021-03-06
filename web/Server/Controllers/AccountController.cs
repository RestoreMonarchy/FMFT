using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Shared.Models.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : RESTFulController
    {
        private readonly IUserProcessingService userService;

        public AccountController(IUserProcessingService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("info")]
        public async ValueTask<IActionResult> Info()
        {
            UserInfo userInfo = await userService.GetAuthenticatedUserInfoAsync();
            return Ok(userInfo);
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterUserWithPasswordModel model)
        {
            try
            {
                UserInfo userInfo = await userService.RegisterUserWithPasswordAsync(model);
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
                UserInfo userInfo = await userService.SignInUserWithPasswordAsync(model);
                return Ok(userInfo);
            } catch (UserPasswordNotMatchException exception)
            {
                return Forbidden(exception);
            }
        }

        [HttpGet("logout")]
        public async ValueTask<IActionResult> LogOut([FromQuery] string returnUrl = "/")
        {
            await userService.SignOutUserAsync();
            return LocalRedirect(returnUrl);
        }
    }
}
