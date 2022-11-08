using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FMFT.Web.Server.Models.Shows
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public int AuditoriumId { get; set; }

        public List<ShowReservedSeat> ReservedSeats { get; set; }
    }
}
