using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;
using System.Security.Claims;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public partial class AccountService : IAccountService
    {
        private readonly IAuthenticationBroker authenticationBroker;
        private readonly ILoggingBroker loggingBroker;

        public AccountService(IAuthenticationBroker authenticationBroker, ILoggingBroker loggingBroker)
        {
            this.authenticationBroker = authenticationBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Account> RetrieveAccountAsync()
            => TryCatch(() =>
            {
                ClaimsPrincipal claimsPrincipal = authenticationBroker.GetClaimsPrincipal();
                Account account = MapClaimsPrincipalToAccount(claimsPrincipal);
                
                return ValueTask.FromResult(account);
            });

        public ValueTask<string> CreateTokenAsync(CreateTokenParams @params)
            => TryCatch(() =>
            {
                Dictionary<string, object> claimsDict = MapAccountToDictionary(@params.Account);
                string token = authenticationBroker.CreateToken(claimsDict);
                return ValueTask.FromResult(token);
            });
    }
}
