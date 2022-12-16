using System.Text.Json.Serialization;

namespace FMFT.Web.Client.Models.API.Reservations
{
    public class ReservationDetails
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
