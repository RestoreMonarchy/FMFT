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
                ReservationBulkItems = new(),
                Attachments = new()
            };

            foreach (ReservationItem item in reservation.Items.Where(x => x.Seat != null))
            {
                @params.ReservationSeats.Add(new()
                {
                    Row = item.Seat.Row,
                    Number = item.Seat.Number,
                    Sector = item.Seat.Sector
                });
            }

            foreach (ReservationItem item in reservation.Items.Where(x => x.Seat == null))
            {
                @params.ReservationBulkItems.Add(new ReservationSummaryEmailParams.ReservationBulkItem()
                {
                    Id = item.Id,
                    ProductName = item.ShowProduct.Name
                });
            }

            return @params;
        }
    }
}
