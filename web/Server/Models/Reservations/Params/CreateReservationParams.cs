namespace FMFT.Web.Server.Models.Reservations.Params
{
    public class CreateReservationParams
    {
        public int ShowId { get; set; }
        public int? UserId { get; set; }

        public List<int> SeatIds { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
