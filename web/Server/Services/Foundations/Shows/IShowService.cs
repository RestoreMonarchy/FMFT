using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Services.Foundations.Shows
{
    public interface IShowService
    {
        ValueTask<Show> AddShowAsync(AddShowParams @params);
        ValueTask<Show> ModifyShowAsync(UpdateShowParams @params);
        ValueTask<Show> ModifyShowSellingDetailsAsync(UpdateShowSellingDetailsParams @params);
        ValueTask<Show> ModifyShowStatusAsync(UpdateShowStatusParams @params);
        ValueTask<Show> ModifyShowTimeAsync(UpdateShowTimeParams @params);
        ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync();
        ValueTask<Show> RetrievePublicShowByIdAsync(int showId);
        ValueTask<IEnumerable<Show>> RetrievePublicShowsAsync();
        ValueTask<Show> RetrieveShowByIdAsync(int showId);
    }
}
