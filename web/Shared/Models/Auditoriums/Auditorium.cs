using FMFT.Web.Shared.Models.Seats;

namespace FMFT.Web.Shared.Models.Auditoriums
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Seat> Seats { get; set; }
    }
}
