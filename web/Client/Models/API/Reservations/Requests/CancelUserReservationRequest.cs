namespace FMFT.Web.Client.Models.API.Reservations.Requests
{
    public class CancelUserReservationRequest
    {
        public string ReservationId { get; set; }
        public int UserId { get; set; }
    }
}
