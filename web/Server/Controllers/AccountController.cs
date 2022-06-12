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
        public async ValueTask<IActionResult> Register(
            [FromBody] RegisterUserWithPasswordModel model,
            [FromQuery] string returnUrl = "/")
        {
            try
            {
                await userService.RegisterUserWithPasswordAsync(model);
            } catch (UserPasswordInvalidException)
            {
                return Problem();
            } catch (UserEmailAlreadyExistsException)
            {
                return Conflict();
            }

            return Ok();
        } 

        [HttpPost("login")]
        public async ValueTask<IActionResult> LogIn(
            [FromBody] SignInUserWithPasswordModel model, 
            [FromQuery] string returnUrl = "/")
        {
            try
            {
                await userService.SignInUserWithPasswordAsync(model);
            } catch (UserPasswordNotMatchException)
            {
                return BadRequest();
            }

            return LocalRedirect(returnUrl);
        }

        [HttpGet("logout")]
        public async ValueTask<IActionResult> LogOut([FromQuery] string returnUrl = "/")
        {
            await userService.SignOutUserAsync();
            return LocalRedirect(returnUrl);
        }
    }
}
