namespace FMFT.Web.Client.Models.API.Reservations.Requests
{
    public class CreateReservationRequest
    {
        public int ShowId { get; set; }
        public int? UserId { get; set; }

        public List<Item> Items { get; set; }
        public class Item
        {
            public int ShowProductId { get; set; }
            public int? SeatId { get; set; }
        }
            
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
