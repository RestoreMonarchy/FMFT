using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReservationsController : RESTFulController
    {
        private readonly IReservationProcessingService reservationService;

        public ReservationsController(IReservationProcessingService reservationService)
        {
            this.reservationService = reservationService;
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
    }
}
