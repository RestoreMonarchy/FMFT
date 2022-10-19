using FMFT.Web.Client.Models.API.Accounts;

namespace FMFT.Web.Client.Services.Accounts
{
    public interface IAccountService
    {
        ValueTask InitializeAsync();
        ValueTask LoginAsync(AccountToken accountToken);
    }
}