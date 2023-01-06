﻿using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Params;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Services.Orchestrations.Shows
{
    public interface IShowOrchestrationService
    {
        ValueTask<Show> AddShowAsync(AddShowParams @params);
        ValueTask<ShowProduct> AddShowProductAsync(AddShowProductParams @params);
        ValueTask<Show> ModifyShowAsync(UpdateShowParams @params);
        ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync();
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
        ValueTask<IEnumerable<ShowProduct>> RetrieveShowProductsByShowIdAsync(int showId);
        ValueTask<ShowProduct> ModifyShowProductAsync(UpdateShowProductParams @params);
    }
}