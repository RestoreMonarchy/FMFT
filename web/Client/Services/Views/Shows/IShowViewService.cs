using FMFT.Web.Shared.Models.Shows;

namespace FMFT.Web.Client.Services.Views.Shows
{
    public interface IShowViewService
    {
        ValueTask<List<Show>> RetrieveAllShowsAsync();
    }
}