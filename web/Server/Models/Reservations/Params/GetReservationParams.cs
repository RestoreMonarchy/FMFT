namespace FMFT.Web.Server.Models.Reservations.Params
{
    public class GetReservationParams
    {
        public string? ReservationId { get; set; }
        public int? ShowId { get; set; }
        public int? UserId { get; set; }
        public int? OrderId { get; set; }
    }
}
