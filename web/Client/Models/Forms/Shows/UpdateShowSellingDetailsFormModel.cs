using System.ComponentModel.DataAnnotations;

namespace FMFT.Web.Client.Models.Forms.Shows
{
    public class UpdateShowSellingDetailsFormModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public DateOnly SellStartDate { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TimeOnly SellStartTime { get; set; }
    }
}
