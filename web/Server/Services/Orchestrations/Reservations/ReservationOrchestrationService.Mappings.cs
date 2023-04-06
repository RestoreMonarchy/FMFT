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

            foreach (ReservationItem item in reservation.Items)
            {
                @params.ReservationSeats.Add(new()
                {
                    Row = item.Seat.Row,
                    Number = item.Seat.Number,
                    Sector = item.Seat.Sector
                });
            }

            return @params;
        }
    }
}
