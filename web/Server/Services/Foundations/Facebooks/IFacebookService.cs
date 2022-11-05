using FMFT.Web.Server.Models.Facebooks;

namespace FMFT.Web.Server.Services.Foundations.Facebooks
{
    public interface IFacebookService
    {
        ValueTask<FacebookUser> GetFacebookUserAsync(string accessToken);
    }
}
