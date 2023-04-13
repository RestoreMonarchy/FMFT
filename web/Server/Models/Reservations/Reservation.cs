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
        public bool IsValid { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? UpdateStatusDate { get; set; }

        public ShowInfo Show { get; set; }
        public UserInfo User { get; set; }

        public ReservationDetails Details { get; set; }
        public List<ReservationItem> Items { get; set; }

        [JsonIgnore]
        public IEnumerable<ReservationItem> SeatItems => Items.Where(x => x.Seat != null);
        [JsonIgnore]
        public IEnumerable<ReservationItem> BulkItems => Items.Where(x => x.Seat == null);

        [JsonIgnore]
        public Guid SecretCode { get; set; }

        public int UserId() => User?.Id ?? 0;
    }
}
