using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.ShowProducts;
using System.Text.Json.Serialization;

namespace FMFT.Web.Server.Models.Reservations
{
    public class ReservationItem
    {
        public int Id { get; set; }
        public ShowProduct ShowProduct { get; set; }
        public Seat Seat { get; set; }
        public bool IsScanned { get; set; }
        public DateTimeOffset? ScanDate { get; set; }

        [JsonIgnore]
        public Guid SecretCode { get; set; }
    }
}