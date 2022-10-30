﻿using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;

namespace FMFT.Web.Server.Controllers.Users
{
    public partial class UsersController
    {
        [HttpPost("{userId}/reservations/create")]
        public async ValueTask<IActionResult> CreateReservation(int userId, [FromBody] CreateReservationParams @params)
        {
            @params.UserId = userId;

            try
            {
                Reservation reservation = await reservationCoordinationService.CreateReservationAsync(@params);

                return Ok(reservation);
            }
            catch (SeatAlreadyReservedReservationException exception)
            {
                return Conflict(exception);
            }
            catch (UserAlreadyReservedReservationException exception)
            {
                return Conflict(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{userId}/reservations")]
        public async ValueTask<IActionResult> GetUserReservations(int userId)
        {
            try
            {
                IEnumerable<Reservation> reservations = await reservationCoordinationService.RetrieveReservationsByUserIdAsync(userId);

                return Ok(reservations);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
        }

        [HttpPost("{userId}/reservations/{reservationId}/cancel")]
        public async ValueTask<IActionResult> CancelReservationAsync(string reservationId)
        {
            try
            {
                Reservation reservation = await reservationCoordinationService.CancelReservationAsync(reservationId);

                return Ok(reservation);
            }
            catch (NotFoundReservationException exception)
            {
                return NotFound(exception);
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
