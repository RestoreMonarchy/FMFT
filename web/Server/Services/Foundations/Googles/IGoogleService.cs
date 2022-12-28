using FMFT.Web.Server.Models.Googles;

namespace FMFT.Web.Server.Services.Foundations.Googles
{
    public interface IGoogleService
    {
        ValueTask<GoogleUser> GetGoogleUserAsync(string credentials);
    }
}