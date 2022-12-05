using FMFT.Web.Server.Models.Seats;
using System.Text.Json.Serialization;

namespace FMFT.Web.Server.Models.Reservations
{
    public class ReservationSeat
    {
        public int Id { get; set; }
        public Seat Seat { get; set; }

        [JsonIgnore]
        public Guid SecretCode { get; set; }
    }
}