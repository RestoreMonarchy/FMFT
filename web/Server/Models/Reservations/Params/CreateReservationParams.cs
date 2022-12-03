namespace FMFT.Web.Server.Models.Reservations.Params
{
    public class CreateReservationParams
    {
        public int ShowId { get; set; }
        public int UserId { get; set; }

        public List<int> SeatIds { get; set; }
    }
}
