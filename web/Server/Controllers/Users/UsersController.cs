using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Services.Orchestrations.AccountReservations;
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
        private readonly IAccountReservationOrchestrationService reservationService;

        public UsersController(IAccountReservationOrchestrationService reservationService, IUserAccountOrchestrationService userAccountService)
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
            } catch (NotFoundUserException exception)
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

        [HttpPost("{userId}/updaterole")]
        public async ValueTask<IActionResult> UpdateUserRole(int userId, UpdateUserRoleParams @params)
        {
            try
            {
                @params.UserId = userId;
                await userAccountService.UpdateUserRoleAsync(@params);
                return Ok();
            } catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            } catch (AccountNotAuthorizedException exception)
            {
                return Forbidden(exception);
            } catch (NotFoundUserException exception)
            {
                return NotFound(exception);
            } catch (AlreadyExistsUserRoleException exception)
            {
                return Conflict(exception);
            }
        }

        [HttpPost("{userId}/updateculture")]
        public async ValueTask<IActionResult> UpdateUserCulture(int userId, UpdateUserCultureParams @params)
        {
            try
            {
                @params.UserId = userId;
                await userAccountService.UpdateUserCultureAsync(@params);
                return Ok();
            }
            catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            }
            catch (AccountNotAuthorizedException exception)
            {
                return Forbidden(exception);
            }
            catch (NotFoundUserException exception)
            {
                return NotFound(exception);
            }
            catch (AlreadyExistsUserCultureException exception)
            {
                return Conflict(exception);
            }
        }
    }
}
