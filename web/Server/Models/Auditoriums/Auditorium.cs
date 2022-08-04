using FMFT.Web.Server.Models.Seats;

namespace FMFT.Web.Server.Models.Auditoriums
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Seat> Seats { get; set; }
    }
}
