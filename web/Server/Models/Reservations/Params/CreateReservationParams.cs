namespace FMFT.Web.Server.Models.Reservations.Params
{
    public class CreateReservationParams
    {
        public int ShowId { get; set; }
        public int? UserId { get; set; }
                
        public class ReservationItem
        {
            public int ShowProductId { get; set; }
            public int? SeatId { get; set; }
        }

        public List<ReservationItem> Items { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
