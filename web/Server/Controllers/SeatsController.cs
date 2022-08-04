using FMFT.Web.Server.Services.Foundations.Seats;
using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Seats.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : RESTFulController
    {
        private readonly ISeatService seatService;

        public SeatsController(ISeatService seatService)
        {
            this.seatService = seatService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetSeats()
        {
            IEnumerable<Seat> seats = await seatService.RetrieveAllSeatsAsync();
            return Ok(seats);
        }

        [HttpGet("{seatId}")]
        public async ValueTask<IActionResult> GetSeat(int seatId)
        {
            try
            {
                Seat seat = await seatService.RetrieveSeatByIdAsync(seatId);
                return Ok(seat);
            } catch (SeatNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
