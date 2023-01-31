using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Helpers
{
    public class FormatHelper
    {
        public static string OrderId(int orderId)
        {
            return orderId.ToString().PadLeft(6, '0');
        }

        public static string TranslatePaymentMethod(PaymentMethod paymentMethod)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.Blik:
                    return "Blik";
                case PaymentMethod.Przelewy24:
                    return "Przelewy24";
                case PaymentMethod.Mock:
                    return "Mock";
                case PaymentMethod.CreditDebitCard:
                    return "Karta płatnicza";
                default:
                    return "???";
            }
        }

        public static string TranslateOrderStatus(OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatus.PaymentWaiting:
                    return "Oczekiwanie na płatność";
                case OrderStatus.PaymentReceived:
                    return "Otrzymano płatność";
                case OrderStatus.Completed:
                    return "Zakończone";
                case OrderStatus.Expired:
                    return "Nie zapłacone";
                default:
                    return "???";
            }
        }

        public static string TranslateReservationStatus(ReservationStatus reservationStatus)
        {
            switch (reservationStatus)
            {
                case ReservationStatus.Pending:
                    return "Oczekiwanie na płatność";
                case ReservationStatus.Ok:
                    return "Ważna";
                case ReservationStatus.Expired:
                    return "Nie opłacona";
                case ReservationStatus.Canceled:
                    return "Anulowana";
                default:
                    return "???";
            }
        }
    }
}
