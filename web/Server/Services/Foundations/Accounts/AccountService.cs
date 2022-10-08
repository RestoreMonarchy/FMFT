using FMFT.Extensions.Authentication.Models;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;
using System.Security.Claims;
using System.Security.Principal;

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

        public ValueTask<string> CreateTokenAsync(Account account)
            => TryCatch(() =>
            {
                Dictionary<string, object> claimsDict = MapAccountToDictionary(account);
                string token = authenticationBroker.CreateToken(claimsDict);
                return ValueTask.FromResult(token);
            });

        public ValueTask SignOutAccountAsync()
            => TryCatch(async () =>
            {
                await authenticationBroker.SignOutAsync();
            });

        public ValueTask ChallengeExternalLoginAsync(ChallengeExternalLoginParams @params)
            => TryCatch(async () =>
            {
                await authenticationBroker.ChallengeExternalLoginAsync(@params.Provider, @params.RedirectUrl);
            });

        public ValueTask<ExternalLogin> RetrieveExternalLoginAsync()
            => TryCatch(async () => 
            {
                ExternalLoginInfo externalLoginInfo = await authenticationBroker.GetExternalLoginInfoAsync();
                ExternalLogin externalLogin = MapExternalLoginInfoToExternalLogin(externalLoginInfo);

                return externalLogin;
            });

        public ValueTask SignInAccountAsync(SignInAccountParams @params)
            => TryCatch(async () =>
            {
                Dictionary<string, object> claims = MapAccountToDictionary(@params.Account);

                await authenticationBroker.SignInAsync(claims, @params.IsPersistent, @params.AuthenticationMethod);
            });
    }
}
