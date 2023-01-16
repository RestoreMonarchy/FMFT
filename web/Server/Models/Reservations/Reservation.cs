using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Shared.Enums;
using System.Text.Json.Serialization;

namespace FMFT.Web.Server.Models.Reservations
{
    public class Reservation
    {
        public string Id { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? UpdateStatusDate { get; set; }

        public Show Show { get; set; }
        public UserInfo User { get; set; }
        public UserInfo AdminUser { get; set; }

        public ReservationDetails Details { get; set; }
        public List<ReservationSeat> Seats { get; set; }

        [JsonIgnore]
        public Guid SecretCode { get; set; }

        public int UserId() => User?.Id ?? 0;
    }
}
