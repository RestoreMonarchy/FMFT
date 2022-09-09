using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Services.Orchestrations.AccountReservations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : RESTFulController
    {
        private readonly IAccountReservationOrchestrationService accountReservationService;

        public ReservationsController(IAccountReservationOrchestrationService accountReservationService)
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
            } catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            } catch (AccountNotAuthorizedException exception)
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
            } catch (ReservationNotFoundException exception)
            {
                return NotFound(exception);
            } catch (AccountNotAuthenticatedException exception)
            {
                return Unauthorized(exception);
            } catch (AccountNotAuthorizedException exception)
            {
                return Forbidden(exception);
            }
        }

        [HttpPost("{reservationId}/status")]
        public async ValueTask<IActionResult> UpdateReservationStatus(int reservationId, [FromBody] UpdateReservationStatusRequest request)
        {
            try
            {
                request.ReservationId = reservationId;
                Reservation reservation = await accountReservationService.UpdateReservationStatusAsync(request);
                return Ok(reservation);
            }
            catch (ReservationNotFoundException exception)
            {
                return NotFound(exception);
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
