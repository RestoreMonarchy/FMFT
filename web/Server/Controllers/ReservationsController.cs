using FMFT.Web.Server.Services.Orchestrations.UserReservations;
using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Exceptions;
using FMFT.Web.Shared.Models.Reservations.Models;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class ReservationsController : RESTFulController
    {
        private readonly IReservationProcessingService reservationService;
        private readonly IUserReservationOrchestrationService userReservationService;

        public ReservationsController(IReservationProcessingService reservationService, 
            IUserReservationOrchestrationService userReservationService)
        {
            this.reservationService = reservationService;
            this.userReservationService = userReservationService;
        }

        [HttpGet("reservations")]
        public async ValueTask<IActionResult> GetAllReservations()
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("reservations/{reservationId}")]
        public async ValueTask<IActionResult> GetReservation(int reservationId)
        {
            try
            {
                Reservation reservation = await reservationService.RetrieveReservationByIdAsync(reservationId);
                return Ok(reservation);
            } catch (ReservationNotFoundException e)
            {
                return NotFound(e);
            }            
        }

        [HttpPost("users/{userId}/reservations/create")]
        public async ValueTask<IActionResult> CreateReservation(int userId, [FromBody] CreateReservationModel model)
        {
            model.UserId = userId;

            try
            {
                Reservation reservation = await userReservationService.CreateReservationAsync(model);
                return Ok(reservation);
            } catch (SeatAlreadyReservedException exception)
            {
                return Conflict(exception);
            } catch (UserAlreadyReservedException exception)
            {
                return Forbidden(exception);
            }            
        }
    }
}
