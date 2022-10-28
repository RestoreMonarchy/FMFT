using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : RESTFulController
    {
        private readonly IReservationOrchestrationService accountReservationService;

        public ReservationsController(IReservationOrchestrationService accountReservationService)
        {
            this.accountReservationService = accountReservationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllReservations()
        {
            try
            {
                IEnumerable<Reservation> reservations = await accountReservationService.RetrieveAllReservationsAsync();
                return Ok(reservations);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }            
        }

        [HttpGet("{reservationId}")]
        public async ValueTask<IActionResult> GetReservation(int reservationId)
        {
            try
            {
                Reservation reservation = await accountReservationService.RetrieveReservationByIdAsync(reservationId);
                return Ok(reservation);
            } catch (NotFoundReservationException exception)
            {
                return NotFound(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
        }

        [HttpPost("{reservationId}/updatestatus")]
        public async ValueTask<IActionResult> UpdateReservationStatus(int reservationId, [FromBody] UpdateReservationStatusRequest request)
        {
            try
            {
                request.ReservationId = reservationId;
                Reservation reservation = await accountReservationService.UpdateReservationStatusAsync(request);
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
