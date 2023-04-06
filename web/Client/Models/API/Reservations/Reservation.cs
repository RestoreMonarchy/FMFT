using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Shared.Enums;
using System.Text.Json.Serialization;

namespace FMFT.Web.Client.Models.API.Reservations
{
    public class Reservation
    {
        public string Id { get; set; }
        public bool IsValid { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public Show Show { get; set; }
        public UserInfo User { get; set; }
        public ReservationDetails Details { get; set; }
        public List<ReservationItem> Items { get; set; }
        public IEnumerable<ReservationItem> Seats => Items.Where(x => !x.IsBulk);
        public IEnumerable<ReservationItem> BulkItems => Items.Where(x => x.IsBulk);

        [JsonIgnore]
        public bool IsNotValid => Status != ReservationStatus.Ok;
        public string Email() => User != null ? User.Email : Details?.Email ?? null;
    }
}
