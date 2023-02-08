using FMFT.Extensions.Payments.Models.Arguments;
using FMFT.Extensions.Payments.Models.Enums;
using FMFT.Extensions.Payments.Models.Exceptions;
using FMFT.Extensions.Payments.Models.Options;
using FMFT.Extensions.Payments.Models.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        private readonly Przelewy24Options options;

        private readonly string baseUrl;
        private Przelewy24 Client => new(GetBaseP24Url(options.UseSandbox), options.UserName, options.UserSecret, options.CRC);

        public Przelewy24PaymentProvider(IOptions<Przelewy24Options> options,
            IConfiguration configuration)

        {
            this.options = options.Value;
            this.baseUrl = configuration["BaseUrl"].TrimEnd('/');
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

        public ValueTask<ProcessPaymentNotificationResult> ProcessPaymentNotificationAsync(ProcessPaymentNotificationArguments arguments)
        {
            ProcessPaymentNotificationResult result = new()
            {
            };

            return ValueTask.FromResult(result);
        }

        public async ValueTask<RegisterPaymentResult> RegisterPaymentAsync(RegisterPaymentArguments arguments)
        {

            Przelewy24 client = Client;

            P24TransactionRequest request = new()
            {
                MerchantId = options.MerchantId,
                PosId = options.PosId,
                SessionId = arguments.SessionId.ToString(),
                Amount = (int)(arguments.Amount * 100),
                Currency = arguments.Currency,
                Description = $"Order #{arguments.OrderId} - {arguments.CustomerFirstName} {arguments.CustomerLastName}",
                Email = arguments.CustomerEmailAddress,
                Country = "PL",
                Language = "pl",
                UrlReturn = string.Concat(baseUrl, "/orders/", arguments.OrderId),
                UrlStatus = string.Concat(baseUrl, "/api/orders/pay/przelewy24")
            };

            P24TransactionResponse response = await client.NewTransactionAsync(request);

            string json = JsonConvert.SerializeObject(response);
            Console.WriteLine("[Przelewy24] [Order #{0}] Transaction response:\n{1}", arguments.OrderId, json);

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
