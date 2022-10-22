﻿namespace FMFT.Web.Client.Models.API.Shows.Requests
{
    public class AddShowRequest
    {
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public int AuditoriumId { get; set; }
    }
}