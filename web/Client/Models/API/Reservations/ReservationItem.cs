using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.ShowProducts;

namespace FMFT.Web.Client.Models.API.Reservations
{
    public class ReservationItem
    {
        public int Id { get; set; }
        public ShowProduct ShowProduct { get; set; }
        public Seat Seat { get; set; }
        public bool IsScanned { get; set; }

        public bool IsBulk => Seat == null;
    }
}
