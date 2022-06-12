using FMFT.Web.Server.Services.Processings.Users;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
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

        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterUserWithPasswordModel model)
        {
            try
            {
                await userService.RegisterUserWithPasswordAsync(model);
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

            return Ok();
        } 

        [HttpPost("login")]
        public async ValueTask<IActionResult> LogIn([FromBody] SignInUserWithPasswordModel model)
        {
            try
            {
                await userService.SignInUserWithPasswordAsync(model);
            } catch (UserPasswordNotMatchException exception)
            {
                return Forbidden(exception);
            }

            return Ok();
        }

        [HttpPost("logout")]
        public async ValueTask<IActionResult> LogOut()
        {
            await userService.SignOutUserAsync();
            return Ok();
        }
    }
}
