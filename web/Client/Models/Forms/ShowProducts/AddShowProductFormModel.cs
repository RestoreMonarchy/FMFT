using System.ComponentModel.DataAnnotations;

namespace FMFT.Web.Client.Models.Forms.ShowProducts
{
    public class AddShowProductFormModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(0.01, 1000, ErrorMessage = "Cena musi być większa od 0.01 i mniejsza od 1000")]
        public decimal? Price { get; set; }

        [Range(0, 1000, ErrorMessage = "Ilość musi być większa lub równa 0 i mniejsza od 1000")]
        public short Quantity { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsBulk { get; set; }
    }
}
