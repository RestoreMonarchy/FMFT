﻿using FMFT.Web.Client.Models.Shows;

namespace FMFT.Web.Client.Services.Foundations.Shows
{
    public interface IShowService
    {
        ValueTask<List<Show>> RetrieveAllShowsAsync();
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
    }
}
