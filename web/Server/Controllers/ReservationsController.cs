using FMFT.Web.Server.Services.Processings.Reservations;
using FMFT.Web.Shared.Models.Reservations;
using FMFT.Web.Shared.Models.Reservations.Exceptions;
using FMFT.Web.Shared.Models.Reservations.Models;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : RESTFulController
    {
        private readonly IReservationProcessingService reservationService;

        public ReservationsController(IReservationProcessingService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllReservations()
        {
            IEnumerable<Reservation> reservations = await reservationService.RetrieveAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{reservationId}")]
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

        [HttpPost("create")]
        public async ValueTask<IActionResult> CreateReservation(CreateReservationModel model)
        {
            try
            {
                Reservation reservation = await reservationService.CreateReservationAsync(model);
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
