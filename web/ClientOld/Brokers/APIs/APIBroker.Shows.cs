﻿using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial class APIBroker
    {
        private const string ShowsRelativeUrl = "api/shows";

        public async ValueTask<Show> GetShowByIdAsync(int showId)
        {
            return await GetAsync<Show>($"{ShowsRelativeUrl}/{showId}");
        }

        public async ValueTask<List<Show>> GetAllShowsAsync()
        {
            return await GetAsync<List<Show>>(ShowsRelativeUrl);
        }

        public async ValueTask<Show> PostShowAsync(PostShowRequest request)
        {
            return await PostAsync<PostShowRequest, Show>(ShowsRelativeUrl, request);
        }

        public async ValueTask<Show> PutShowAsync(PutShowRequest request)
        {
            return await PutAsync<PutShowRequest, Show>(ShowsRelativeUrl, request);
        }
    }
}