using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Coordinations.UserAccountCultures
{
    public interface IUserAccountCultureCoordinationService
    {
        Account RetrieveAccountStore();
        ValueTask<CultureId> RetrieveCultureIdAsync();
        ValueTask SyncUserAccountCulturesAsync();
        ValueTask UpdateCultureAsync(CultureId cultureId);
    }
}