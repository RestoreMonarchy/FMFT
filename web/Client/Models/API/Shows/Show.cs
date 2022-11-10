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

        public TimeSpan Duration() => EndDateTime.Subtract(StartDateTime);

        public string DurationString()
        {
            TimeSpan duration = Duration();
            StringBuilder sb = new();
            if (duration.Hours > 0)
                sb.Append($"{duration.Hours}h ");

            sb.Append($"{duration.Minutes}m");
            return sb.ToString();
        }
        public string StartDateString() => StartDateTime.LocalDateTime.ToString("f");
        public string EndDateString() => EndDateTime.LocalDateTime.ToString("f");
    }
}
