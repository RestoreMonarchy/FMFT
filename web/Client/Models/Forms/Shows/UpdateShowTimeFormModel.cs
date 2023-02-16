using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FMFT.Web.Client.Models.Forms.Shows
{
    public class UpdateShowTimeFormModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        public int DurationMinutes { get; set; }
    }
}
