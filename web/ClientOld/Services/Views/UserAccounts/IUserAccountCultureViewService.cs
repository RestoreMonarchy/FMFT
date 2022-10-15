using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Views.UserAccountCultures
{
    public interface IUserAccountCultureViewService
    {
        ValueTask ChangeCultureAsync(CultureId cultureId);
        ValueTask<CultureId> RetrieveCultureIdAsync();
    }
}