﻿namespace FMFT.Web.Client.Models.Reservations.Requests
{
    public class CreateReservationRequest
    {
        public int ShowId { get; set; }
        public int SeatId { get; set; }
        public int UserId { get; set; }
    }
}