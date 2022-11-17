using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Services.Coordinations.Reservations;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : RESTFulController
    {
        private readonly IReservationCoordinationService reservationCoordinationService;

        public ReservationsController(IReservationCoordinationService reservationCoordinationService)
        {
            this.reservationCoordinationService = reservationCoordinationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllReservations()
        {
            try
            {
                IEnumerable<Reservation> reservations = await reservationCoordinationService.RetrieveAllReservationsAsync();

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
        public async ValueTask<IActionResult> GetReservation(string reservationId)
        {
            try
            {
                Reservation reservation = await reservationCoordinationService.RetrieveReservationByIdAsync(reservationId);

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

        [HttpGet("{reservationId}/image")]
        public async ValueTask<IActionResult> GetReservationImage(string reservationId)
        {
            try
            {
                QRCodeImage image = await reservationCoordinationService.GenerateReservationQRCodeImageAsync(reservationId);

                return File(image.Data, image.ContentType);
            }
            catch (NotFoundReservationException exception)
            {
                return NotFound(exception);
            }
        }

        //[HttpPost("{reservationId}/updatestatus")]
        //public async ValueTask<IActionResult> UpdateReservationStatus(int reservationId, [FromBody] UpdateReservationStatusRequest request)
        //{
        //    try
        //    {
        //        request.ReservationId = reservationId;
        //        Reservation reservation = await reservationCoordinationService.UpdateReservationStatusAsync(request);

        //        return Ok(reservation);
        //    }
        //    catch (NotFoundReservationException exception)
        //    {
        //        return NotFound(exception);
        //    }
        //    catch (NotAuthenticatedAccountException exception)
        //    {
        //        return Unauthorized(exception);
        //    }
        //    catch (NotAuthorizedAccountException exception)
        //    {
        //        return Forbidden(exception);
        //    }
        //}        
    }
}
