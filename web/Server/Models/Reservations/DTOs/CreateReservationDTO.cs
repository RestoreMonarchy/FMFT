namespace FMFT.Web.Server.Models.Reservations.DTOs
{
    public class CreateReservationDTO
    {
        public int ShowId { get; set; }
        public int? UserId { get; set; }
        public string Items { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
