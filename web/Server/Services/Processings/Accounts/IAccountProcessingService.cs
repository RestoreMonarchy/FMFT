using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        ValueTask AuthorizeAccountByUserIdAsync(int authorizedUserId);
        ValueTask AuthorizeAccountAsync();
        ValueTask<Account> RetrieveAccountAsync();        
        ValueTask<AccountToken> CreateTokenAsync(CreateTokenParams @params);
    }
}
