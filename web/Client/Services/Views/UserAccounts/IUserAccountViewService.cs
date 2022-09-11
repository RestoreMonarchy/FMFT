using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Views.UserAccounts
{
    public interface IUserAccountViewService
    {
        ValueTask ChangeCultureAsync(CultureId cultureId);
        Account RetrieveAccount();
    }
}