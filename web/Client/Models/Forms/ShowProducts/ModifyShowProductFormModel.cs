using System.ComponentModel.DataAnnotations;

namespace FMFT.Web.Client.Models.Forms.ShowProducts
{
    public class ModifyShowProductFormModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(0.01, 1000, ErrorMessage = "Cena musi być większa od 0.01 i mniejsza od 1000")]
        public decimal? Price { get; set; }

        public bool IsEnabled { get; set; }
    }
}
