using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Users.Exceptions;
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
                Reservation reservation = await reservationService.CreateReservationAsync(request);
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
                IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByUserIdAsync(userId);
                return Ok(reservations);
            }
            catch (UserNotAuthorizedException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
