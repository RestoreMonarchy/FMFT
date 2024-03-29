﻿namespace FMFT.Web.Client.Models.API.Seats
{
    public class Seat
    {
        public int Id { get; set; }
        public short Row { get; set; }
        public short Number { get; set; }
        public char Sector { get; set; }

        public string SectorString => Sector == 'A' ? "Parter" : "Balkon";
    }
}
