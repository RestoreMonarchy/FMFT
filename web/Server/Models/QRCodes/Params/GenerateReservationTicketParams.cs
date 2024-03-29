﻿namespace FMFT.Web.Server.Models.QRCodes.Params
{
    public class GenerateReservationTicketParams
    {
        public string ReservationId { get; set; }
        public string ShowName { get; set; }
        public string SeatInformation { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid SecretCode { get; set; }
    }
}
