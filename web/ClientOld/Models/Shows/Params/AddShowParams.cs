﻿namespace FMFT.Web.Client.Models.Shows.Params
{
    public class AddShowParams
    {
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public int AuditoriumId { get; set; }
    }
}