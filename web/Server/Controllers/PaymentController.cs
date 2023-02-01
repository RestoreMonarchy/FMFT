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
        private readonly OrderCoordinationService orderCoordinationService;

        public PaymentController(OrderCoordinationService orderCoordinationService)
        {
            this.orderCoordinationService = orderCoordinationService;
        }

        [HttpPost("notifications/{paymentProvider}")]
        public async ValueTask ProcessNotifications(PaymentProvider paymentProvider)
        {
            await orderCoordinationService.ProcessPaymentNotificationAsync(paymentProvider);
        }
    }
}
