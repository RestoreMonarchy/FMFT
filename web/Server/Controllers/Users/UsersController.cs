using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Services.Coordinations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System;

namespace FMFT.Web.Server.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : RESTFulController
    {
        private readonly IUserAccountOrchestrationService userAccountOrchestrationService;
        private readonly IReservationCoordinationService reservationCoordinationService;

        public UsersController(
            IReservationCoordinationService reservationCoordinationService, 
            IUserAccountOrchestrationService userAccountOrchestrationService)
        {
            this.reservationCoordinationService = reservationCoordinationService;
            this.userAccountOrchestrationService = userAccountOrchestrationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetUsers()
        {
            try
            {
                IEnumerable<User> users = await userAccountOrchestrationService.RetrieveAllUsersAsync();

                return Ok(users);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{userId}")]
        public async ValueTask<IActionResult> GetUser(int userId)
        {
            try
            {
                User user = await userAccountOrchestrationService.RetrieveUserByIdAsync(userId);

                return Ok(user);
            } catch (NotFoundUserException exception)
            {
                return NotFound(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
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
                await userAccountOrchestrationService.UpdateUserRoleAsync(@params);

                return Ok();
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotAuthorizedAccountException exception)
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
                await userAccountOrchestrationService.UpdateUserCultureAsync(@params);

                return Ok();
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
            catch (NotAuthorizedAccountException exception)
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

        [HttpGet("{userId}/confirmemail/{confirmSecret}")]
        public async ValueTask<IActionResult> ConfirmUserEmail(int userId, Guid confirmSecret)
        {
            try
            {
                await userAccountOrchestrationService.ConfirmEmailAsync(userId, confirmSecret);

                return Ok();
            }
            catch (AlreadyConfirmedEmailUserException exception)
            {
                return Conflict(exception);
            }
            catch (NotMatchConfirmEmailSecretUserException exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
