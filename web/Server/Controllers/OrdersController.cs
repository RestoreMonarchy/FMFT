using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Services.Coordinations.Reservations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : RESTFulController
    {
        private readonly IReservationCoordinationService reservationCoordinationService;

        public OrdersController(IReservationCoordinationService reservationCoordinationService)
        {
            this.reservationCoordinationService = reservationCoordinationService;
        }

        [HttpGet("{orderId}/reservations")]
        public async ValueTask<IActionResult> GetOrderReservations(int orderId)
        {
            try
            {
                IEnumerable<Reservation> reservations = await reservationCoordinationService.RetrieveReservationsByOrderIdAsync(orderId);

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