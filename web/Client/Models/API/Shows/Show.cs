using System.Text;

namespace FMFT.Web.Client.Models.API.Shows
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public int AuditoriumId { get; set; }
        public Guid? ThumbnailMediaId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public List<ShowReservedSeat> ReservedSeats { get; set; }


        public bool IsPast() => StartDateTime.UtcDateTime < DateTime.UtcNow;
        public TimeSpan Duration() => EndDateTime - StartDateTime;
    }
}
