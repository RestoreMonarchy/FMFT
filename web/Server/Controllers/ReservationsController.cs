using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.QRCodes;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Params;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Server.Models.Reservations.Results;
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

        [HttpGet("{reservationId}/qrcode")]
        public async ValueTask<IActionResult> GetReservationQrCode(string reservationId)
        {
            try
            {
                QRCodeImage image = await reservationCoordinationService.GenerateReservationQRCodeImageAsync(reservationId);

                return Ok(image);
            }
            catch (NotFoundReservationException exception)
            {
                return NotFound(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{reservationId}/seats/{reservationSeatId}/qrcode")]
        public async ValueTask<IActionResult> GetReservationSeatQrCode(string reservationId, int reservationSeatId)
        {
            try
            {
                QRCodeImage image = await reservationCoordinationService.GenerateReservationSeatQRCodeImageAsync(reservationId, reservationSeatId);

                return Ok(image);
            } catch (NotFoundReservationException exception)
            {
                return NotFound(exception);
            } catch (NotFoundSeatReservationException exception)
            {
                return NotFound(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{reservationId}/seats/{reservationSeatId}/ticket")]
        public async ValueTask<IActionResult> GetReservationSeatTicket(string reservationId, int reservationSeatId)
        {
            try
            {
                QRCodeImage image = await reservationCoordinationService.GenerateReservationSeatTicketAsync(reservationId, reservationSeatId);

                return Ok(image);
            }
            catch (NotFoundReservationException exception)
            {
                return NotFound(exception);
            }
            catch (NotFoundSeatReservationException exception)
            {
                return NotFound(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpPost("validate")]
        public async ValueTask<IActionResult> ValidateReservationSecretCodeAsync([FromBody] ValidateReservationSecretCodeParams @params)
        {
            try
            {
                ValidateReservationSecretCodeResult result = await reservationCoordinationService.ValidateReservationSecretCodeAsync(@params.SecretCode);

                return Ok(result);
            }
            catch (NotFoundReservationException exception)
            {
                return NotFound(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
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
