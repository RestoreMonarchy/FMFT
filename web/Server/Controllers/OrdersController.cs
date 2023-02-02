using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Exceptions;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Exceptions;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Services.Coordinations.Orders;
using FMFT.Web.Server.Services.Coordinations.Reservations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : RESTFulController
    {
        private readonly IOrderCoordinationService orderService;
        private readonly IReservationCoordinationService reservationService;
        private readonly ILoggingBroker loggingBroker;

        public OrdersController(IOrderCoordinationService orderService,
            IReservationCoordinationService reservationService,
            ILoggingBroker loggingBroker)
        {
            this.orderService = orderService;
            this.reservationService = reservationService;
            this.loggingBroker = loggingBroker;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllOrders()
        {
            try
            {
                IEnumerable<Order> orders = await orderService.RetrieveAllOrdersAsync();

                return Ok(orders);
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

        [HttpGet("{orderId}")]
        public async ValueTask<IActionResult> GetOrder(int orderId)
        {
            try
            {
                Order order = await orderService.RetrieveOrderByIdAsync(orderId);

                return Ok(order);
            }
            catch (NotFoundOrderException exception)
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

        [HttpGet("session/{sessionId}")]
        public async ValueTask<IActionResult> GetOrderBySessionId(Guid sessionId)
        {
            try
            {
                Order order = await orderService.RetrieveOrderBySessionIdAsync(sessionId);

                return Ok(order);
            }
            catch (NotFoundOrderException exception)
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

        [HttpGet("myorders")]
        public async ValueTask<IActionResult> GetMyOrders()
        {
            try
            {
                IEnumerable<Order> orders = await orderService.RetrieveOrdersForCurrentUserAsync();

                return Ok(orders);
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

        [HttpGet("{orderId}/reservations")]
        public async ValueTask<IActionResult> GetOrderReservations(int orderId)
        {
            try
            {
                IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByOrderIdAsync(orderId);

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

        [HttpGet("{orderId}/pay")]
        public async ValueTask<IActionResult> OrderPay(int orderId)
        {
            try
            {
                PaymentUrl paymentUrl = await orderService.GetOrderPaymentUrlAsync(orderId);

                return Ok(paymentUrl);
            } 
            catch (NotFoundOrderException exception)
            {
                return NotFound(exception);
            }
            catch (NotSupportedProviderPaymentException exception)
            {
                return ServiceUnavailable(exception);
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

        [HttpPost("create")]
        public async ValueTask<IActionResult> CreateOrder([FromBody] CreateOrderParams @params)
        {
            try
            {
                Order order = await orderService.CreateOrderAsync(@params);

                return Ok(order);
            }
            catch (NotSupportedProviderPaymentException exception)
            {
                return ServiceUnavailable(exception);
            }
            catch (CreateUserOrderReservationValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (SeatAlreadyReservedOrderReservationException exception)
            {
                return Conflict(exception);
            }
            catch (SeatsNotProvidedOrderReservationException exception)
            {
                return BadRequest(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
            catch (NotMatchOrderedItemsQtyWithSeatsOrderException exception)
            {
                return BadRequest(exception);
            }
            catch (OrderedQtyTooLargeOrderException exception)
            {
                return BadRequest(exception);
            }
            catch (InvalidShowProductIdOrderException exception)
            {
                return BadRequest(exception);
            }
            catch (OrderAmountInvalidException exception)
            {
                return BadRequest(exception);
            }
            catch (OrderAmountMismatchException exception)
            {
                return BadRequest(exception);
            }
        }
    }
}