namespace FMFT.Web.Client.Models.API.Reservations.Requests
{
    public class CreateUserReservationRequest
    {
        public int ShowId { get; set; }
        public int UserId { get; set; }
        public List<int> SeatIds { get; set; }
    }
}
