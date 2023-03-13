using FMFT.Extensions.Payments.Extensions;
using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Exceptions;
using FMFT.Extensions.Payments.Models.Options;
using FMFT.Extensions.Payments.Models.Results;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P24;

namespace FMFT.Extensions.Payments.Services.Providers
{
    public class Przelewy24PaymentProvider : IPaymentProvider
    {
        public PaymentProviderId Id => PaymentProviderId.Przelewy24;
        private readonly string sandboxUrl =  "https://sandbox.przelewy24.pl";
        private readonly string productionUrl = "https://secure.przelewy24.pl";

        private readonly PaymentProviderOptions generalOptions;
        private readonly Przelewy24Options options;

        private Przelewy24 Client => new(GetBaseP24Url(options.UseSandbox), options.UserName, options.UserSecret, options.CRC);

        public PaymentMethodId[] SupportedPaymentMethodIds => new PaymentMethodId[]
        {
            PaymentMethodId.CreditDebitCard,
            PaymentMethodId.Blik,
            PaymentMethodId.Przelewy24
        };

        public Przelewy24PaymentProvider(IOptions<PaymentProviderOptions> generalOptions, IOptions<Przelewy24Options> options)
        {
            this.generalOptions = generalOptions.Value;
            this.options = options.Value;
        }

        public ValueTask<GetPaymentInfoResult> GetPaymentInfoAsync(GetPaymentInfoArguments arguments)
        {
            GetPaymentInfoResult result = new();

            return ValueTask.FromResult(result);
        }

        public ValueTask<GetPaymentUrlResult> GetPaymentUrlAsync(GetPaymentUrlArguments arguments)
        {
            string url =  string.Concat(GetBaseP24Url(options.UseSandbox), "/trnRequest/", arguments.PaymentToken);

            GetPaymentUrlResult result = new()
            {
                Url = url
            };

            return ValueTask.FromResult(result);
        }

        public async ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(ProcessPaymentNotificationArguments arguments)
        {            
            string requestBody = await arguments.HttpContext.Request.ReadBodyToStringAsync();

            P24Notification notification = JsonConvert.DeserializeObject<P24Notification>(requestBody);

            Console.WriteLine("[Przelewy24] Notification Statement: {0}", notification.Statement);
            Console.WriteLine("[Przelewy24] Notification Method Id: {0}", notification.MethodId);
            Console.WriteLine("[Przelewy24] Notification Session Id: {0}", notification.SessionId);
            
            string json = JsonConvert.SerializeObject(notification, Formatting.Indented);

            Console.WriteLine("[Przelewy24] Notification:\n{0}", json);

            Przelewy24 client = Client;

            P24TransactionVerifyRequest request = new()
            {
                OrderId = notification.OrderId,
                MerchantId = notification.MerchantId,
                PosId = notification.PosId,
                Sign = notification.Sign,
                SessionId = notification.SessionId,
                Amount = notification.Amount,
                Currency = notification.Currency
            };

            P24TransactionVerifyResponse response = await client.TransactionVerifyAsync(request);
            if (response.Data == null || response.Data.Status != "success")
            {
                throw new PaymentProviderNotificationException();
            }

            string sessionId = notification.SessionId;
            PaymentStatusId paymentStatus = PaymentStatusId.Completed;
            
            ProcessPaymentNotificationResult result = new()
            {
                SessionId = sessionId,
                PaymentStatus = paymentStatus
            };

            return result;
        }

        public async ValueTask<RegisterPaymentResult> RegisterPaymentAsync(PaymentMethodId paymentMethodId, RegisterPaymentArguments arguments)
        {
            Przelewy24 client = Client;
            int p24Method = paymentMethodId switch
            {
                PaymentMethodId.Przelewy24 => 0,
                PaymentMethodId.Blik => 154,
                PaymentMethodId.CreditDebitCard => 0,
                _ => 0,
            };

            P24TransactionRequest request = new()
            {
                MerchantId = options.MerchantId,
                PosId = options.PosId,
                SessionId = arguments.SessionId.ToString(),
                Amount = (int)(arguments.Amount * 100),
                Currency = arguments.Currency,
                Description = $"Zamówienie #{arguments.OrderId} - {arguments.CustomerFirstName} {arguments.CustomerLastName}",
                Email = arguments.CustomerEmailAddress,
                Country = "PL",
                Language = "pl",
                UrlReturn = generalOptions.GetReturnUrl(arguments.OrderId),
                UrlStatus = generalOptions.GetNotifyUrl("przelewy24"),
                Method = p24Method,
                Channel = 16
            };

            /*
            1 - karty + ApplePay + GooglePay, 
            2 - przelew, 
            4 - tradycyjny przelew, 
            8 - N / A, 
            16 - wszystkie 24 / 7 – udostępnia wszystkie metody płatności, 
            32 - uzyj przedpłaty, 
            64 – tylko metody pay - by - link, 
            128 – formy ratalne, 
            256 – wallety, 
            4096 - karty
            */ 
            string requestJson = JsonConvert.SerializeObject(request);
            Console.WriteLine("[Przelewy24] [Order #{0}] Transaction request: \n{1}", arguments.OrderId, requestJson);

            P24TransactionResponse response = await client.NewTransactionAsync(request);

            string responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine("[Przelewy24] [Order #{0}] Transaction response:\n{1}", arguments.OrderId, responseJson);

            if (response.Data == null)
            {
                Console.WriteLine("[Przelewy24] [Order #{0}] Transaction response token is null in the response", arguments.OrderId);
                throw new PaymentProviderException();
            }

            RegisterPaymentResult result = new()
            {
                Token = response.Data.Token
            };

            return result;
        }

        private string GetBaseP24Url(bool useSandbox)
        {
            if (useSandbox)
                return sandboxUrl;

            return productionUrl;
        }
    }
}
