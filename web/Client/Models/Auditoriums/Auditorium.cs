using FMFT.Web.Client.Models.Seats;

namespace FMFT.Web.Client.Models.Auditoriums
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Seat> Seats { get; set; }
    }
}
