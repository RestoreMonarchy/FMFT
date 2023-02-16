using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Helpers
{
    public class FormatHelper
    {
        public static string OrderId(int orderId)
        {
            return orderId.ToString().PadLeft(6, '0');
        }

        public static string FirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return "???";
            }

            if (firstName.Length > 12)
            {
                firstName = firstName.Substring(0, 9) + "...";
            }

            return firstName;
        }

        public static string LastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return "???";
            }

            if (lastName.Length > 12)
            {
                lastName = lastName.Substring(0, 9) + "...";
            }

            return lastName;
        }
        
        public static string FullName(string firstName, string lastName)
        {
            firstName = FirstName(firstName);
            lastName = LastName(lastName);

            return firstName + " " + lastName;
        }

        public static string Email(string emailAddress)
        {
            if (emailAddress.Length > 25)
            {
                return emailAddress.Substring(0, 22) + "...";
            }

            return emailAddress;
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
