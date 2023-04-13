using FMFT.Web.Shared.Enums;
using System.Text.Json.Serialization;

namespace FMFT.Web.Client.Models.Services.Orders
{
    public class OrderStateData
    {
        public const int CurrentVersion = 2;

        public bool IsValid()
        {
            if (CurrentVersion != Version)
            {
                return false;
            }

            if (ExpireDate <= DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }

        public int ShowId { get; set; }
        public string CurrentStepKey { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemStateData> Items { get; set; }

        [JsonIgnore]
        public bool IsAgreeTerms { get; set; }
        [JsonIgnore]
        public bool IsAgreePrzelewy24 { get; set; }

        public DateTime ExpireDate { get; set; }
        public int Version { get; set; }
    }
}
