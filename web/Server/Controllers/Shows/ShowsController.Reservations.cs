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
                IEnumerable<Reservation> reservations = await accountReservationService.RetrieveReservationsByShowIdAsync(showId);
                return Ok(reservations);
            }
            catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            }
            catch (AccountNotAuthorizedException exception)
            {
                return Forbidden(exception);
            }
        }
    }
}
