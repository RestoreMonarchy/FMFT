using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : RESTFulController
    {
        private readonly IUserAccountOrchestrationService userAccountService;
        private readonly IReservationOrchestrationService reservationService;

        public UsersController(IReservationOrchestrationService reservationService, IUserAccountOrchestrationService userAccountService)
        {
            this.reservationService = reservationService;
            this.userAccountService = userAccountService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetUsers()
        {
            try
            {
                IEnumerable<User> users = await userAccountService.RetrieveAllUsersAsync();
                return Ok(users);
            } catch (AccountNotAuthorizedException exception)
            {
                return Forbidden(exception);
            } catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{userId}")]
        public async ValueTask<IActionResult> GetUser(int userId)
        {
            try
            {
                User user = await userAccountService.RetrieveUserByIdAsync(userId);
                return Ok(user);
            } catch (UserNotFoundException exception)
            {
                return NotFound(exception);
            } catch (AccountNotAuthorizedException exception)
            {
                return Forbidden(exception);
            } catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
