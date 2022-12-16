namespace FMFT.Web.Client.Models.API.Reservations.Requests
{
    public class CreateReservationRequest
    {
        public int ShowId { get; set; }
        public int? UserId { get; set; }

        public List<int> SeatIds { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
