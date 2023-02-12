using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Inputs
{
    public partial class PaymentMethodSelect
    {
        [Parameter]
        public PaymentMethod PaymentMethod { get; set; }
        [Parameter]
        public EventCallback<PaymentMethod> PaymentMethodChanged { get; set; }

        private async Task ChangePaymentMethod(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
            await PaymentMethodChanged.InvokeAsync(paymentMethod);
        }

        private string Picture(PaymentMethod paymentMethod)
        {
            return $"/img/payment/{paymentMethod.ToString().ToLower()}.png";
        }

        private bool Checked(PaymentMethod paymentMethod) => PaymentMethod == paymentMethod;

        public bool IsPaymentProviderEnabled(PaymentProvider paymentProvider)
        {
            string[] paymentProviders = Configuration.GetSection("PaymentProviders").Get<string[]>();

            return paymentProviders.Contains(paymentProvider.ToString());
        }
    }
}
