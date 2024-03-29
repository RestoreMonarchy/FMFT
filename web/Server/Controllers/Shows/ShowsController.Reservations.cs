﻿using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FMFT.Web.Server.Controllers.Shows
{
    public partial class ShowsController
    {
        [HttpGet("{showId}/reservations")]
        public async ValueTask<IActionResult> GetShowReservations(int showId)
        {
            try
            {
                IEnumerable<Reservation> reservations = await reservationCoordinationService.RetrieveReservationsByShowIdAsync(showId);
                return Ok(reservations);
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
        }

        [HttpGet("{showId}/users/{userId}/reservations")]
        public async ValueTask<IActionResult> GetUserShowReservations(int showId, int userId)
        {
            try
            {
                IEnumerable<Reservation> reservations = await reservationCoordinationService.RetrieveReservationsByUserAndShowIdAsync(userId, showId);
                return Ok(reservations);
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
        }
    }
}
