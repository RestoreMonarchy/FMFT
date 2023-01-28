using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.UserAccounts.Exceptions;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Models.Users.Requests;
using FMFT.Web.Server.Services.Coordinations.Orders;
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
        private readonly IOrderCoordinationService orderCoordinationService;

        public UsersController(
            IReservationCoordinationService reservationCoordinationService,
            IUserAccountOrchestrationService userAccountOrchestrationService,
            IOrderCoordinationService orderCoordinationService)
        {
            this.reservationCoordinationService = reservationCoordinationService;
            this.userAccountOrchestrationService = userAccountOrchestrationService;
            this.orderCoordinationService = orderCoordinationService;
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

        [HttpPost("{userId}/updatepassword")]
        public async ValueTask<IActionResult> UpdateUserPassword(int userId, UpdateUserPasswordRequest request)
        {
            try
            {
                request.UserId = userId;
                await userAccountOrchestrationService.UpdateUserPasswordAsync(request);

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
            catch (NotPasswordUserException exception)
            {
                return BadRequest(exception);
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

        [HttpPost("{userId}/confirmemail/{confirmSecret}")]
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

        [HttpPost("{userId}/confirmemail/send")]
        public async ValueTask<IActionResult> SendConfirmEmail(int userId)
        {
            try
            {
                await userAccountOrchestrationService.SendUserConfirmAccountEmailAsync(userId);
                    
                return Ok();
            } catch (AlreadyConfirmedEmailUserException exception)
            {
                return Conflict(exception);
            } catch (LimitConfirmEmailUserAccountException exception)
            {
                return Conflict(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{userId}/logins")]
        public async ValueTask<IActionResult> UserLogins(int userId)
        {
            try
            {
                IEnumerable<UserLogin> logins = await userAccountOrchestrationService.RetrieveUserLoginsByUserIdAsync(userId);

                return Ok(logins);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
