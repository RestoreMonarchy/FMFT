using FMFT.Web.Server.Models.Emails.Params;
using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Orders.Params;
using FMFT.Web.Server.Models.Payments;
using FMFT.Web.Server.Models.Payments.Params;
using FMFT.Web.Server.Models.Reservations;
using FMFT.Web.Server.Models.Reservations.Requests;
using FMFT.Web.Shared.Enums;
using Hangfire;

namespace FMFT.Web.Server.Services.Coordinations.Orders
{
    public partial class OrderCoordinationService
    {
        public ValueTask EnqueueExecuteOrderExpireAsync(int orderId, DateTimeOffset expireDate)
        {
            BackgroundJob.Schedule(() => ExecuteOrderExpireAsync(orderId), expireDate);

            return ValueTask.CompletedTask;
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task ExecuteOrderExpireAsync(int orderId)
        {
            Order order = await orderService.RetrieveOrderByIdAsync(orderId);

            if (order.Status != OrderStatus.PaymentWaiting)
            {
                // Order has been completed or canceled already
                return;
            }

            GetPaymentInfoParams @getPaymentInfoParams = new()
            {
                PaymentMethod = order.PaymentMethod,
                SessionId = order.SessionId.ToString(),
                PaymentToken = order.PaymentToken
            };

            PaymentInfo paymentInfo = await paymentService.GetPaymentInfoAsync(@getPaymentInfoParams);

            

            UpdateOrderStatusParams @updateOrderStatusParams = new()
            {
                OrderId = orderId,
                Status = OrderStatus.Expired
            };

            order = await orderService.UpdateOrderStatusAsync(updateOrderStatusParams);

            IEnumerable<Reservation> reservations = await reservationService.RetrieveReservationsByOrderIdAsync(orderId);

            foreach (Reservation reservation in reservations)
            {

            }
        }
    }
}
