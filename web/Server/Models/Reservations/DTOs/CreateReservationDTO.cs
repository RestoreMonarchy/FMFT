namespace FMFT.Web.Server.Models.Reservations.DTOs
{
    public class CreateReservationDTO
    {
        public int ShowId { get; set; }
        public int UserId { get; set; }
        public string Seats { get; set; }
    }
}
