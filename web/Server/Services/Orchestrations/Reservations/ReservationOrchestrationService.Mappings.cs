using FMFT.Web.Server.Models.Emails.Params;
using FMFT.Web.Server.Models.Reservations;

namespace FMFT.Web.Server.Services.Orchestrations.Reservations
{
    public partial class ReservationOrchestrationService
    {
        public ReservationSummaryEmailParams MapReservationToReservationSummaryEmailParams(Reservation reservation)
        {
            ReservationSummaryEmailParams @params = new()
            {
                FirstName = reservation.User?.FirstName ?? reservation.Details?.FirstName ?? null,
                ShowName = reservation.Show.Name,
                ReservationId = reservation.Id,
                ReservationSeats = new(),
                Attachments = new()
            };

            foreach (ReservationSeat reservationSeat in reservation.Seats)
            {
                @params.ReservationSeats.Add(new()
                {
                    Row = reservationSeat.Seat.Row,
                    Number = reservationSeat.Seat.Number,
                    Sector = reservationSeat.Seat.Sector
                });
            }

            return @params;
        }
    }
}
