using FMFT.Web.Client.Models.API.Seats;
using System.ComponentModel.DataAnnotations;

namespace FMFT.Web.Client.Models.Forms.Reservations
{
    public class CreateReservationFormModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int? ShowId { get; set; }

        public int? UserId { get; set; }

        [MinLength(1, ErrorMessage = "Musi być zaznaczone co najmniej jedno miejsce")]
        public List<Seat> Seats { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
