﻿using FMFT.Web.Client.Models.Auditoriums;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<Auditorium> GetAuditoriumByIdAsync(int auditoriumId);
        ValueTask<List<Auditorium>> GetAllAuditoriumsAsync();
    }
}
