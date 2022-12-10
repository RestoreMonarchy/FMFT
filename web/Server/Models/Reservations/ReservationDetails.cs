using Newtonsoft.Json;

namespace FMFT.Web.Server.Models.Reservations
{
    public class ReservationDetails
    {
        [JsonIgnore]
        public string ReservationId { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
