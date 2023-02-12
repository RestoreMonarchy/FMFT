using FMFT.Web.Server.Models.Payments.Exceptions;
using FMFT.Web.Server.Services.Coordinations.Orders;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : RESTFulController
    {
        private readonly IOrderCoordinationService orderCoordinationService;

        public PaymentController(IOrderCoordinationService orderCoordinationService)
        {
            this.orderCoordinationService = orderCoordinationService;
        }

        [HttpPost("notifications/{paymentProvider}")]
        public async ValueTask<IActionResult> ProcessNotifications(PaymentProvider paymentProvider)
        {
            try
            {
                await orderCoordinationService.ProcessPaymentNotificationAsync(paymentProvider);

                return Ok();
            } catch (InvalidNotificationPaymentProviderException exception)
            {
                return BadRequest(exception);
            }
            
        }
    }
}
