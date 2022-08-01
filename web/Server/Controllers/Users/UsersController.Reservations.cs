using FMFT.Web.Shared.Models.Reservations.Exceptions;
using FMFT.Web.Shared.Models.Reservations.Requests;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FMFT.Web.Server.Controllers.Users
{
    public partial class UsersController
    {
        [HttpPost("{userId}/reservations/create")]
        public async ValueTask<IActionResult> CreateReservation(int userId, [FromBody] CreateReservationRequest request)
        {
            request.UserId = userId;

            try
            {
                Reservation reservation = await reservatonService.CreateReservationAsync(request);
                return Ok(reservation);
            }
            catch (SeatAlreadyReservedException exception)
            {
                return Conflict(exception);
            }
            catch (UserAlreadyReservedException exception)
            {
                return Forbidden(exception);
            }
            catch (UserNotAuthorizedException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{userId}/reservations")]
        public async ValueTask<IActionResult> GetUserReservations(int userId)
        {
            try
            {
                IEnumerable<Reservation> reservations = await reservatonService.RetrieveReservationsByUserIdAsync(userId);
                return Ok(reservations);
            }
            catch (UserNotAuthorizedException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
