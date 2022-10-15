using FMFT.Web.Client.Models.API.Seats;

namespace FMFT.Web.Client.Models.API.Auditoriums
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Seat> Seats { get; set; }
    }
}
