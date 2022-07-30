using FMFT.Extensions.Authentication.Models;
using FMFT.Extensions.Authentication.Models.Exceptions;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Shared.Models.Accounts;
using FMFT.Web.Shared.Models.Accounts.Exceptions;
using FMFT.Web.Shared.Models.Accounts.Params;
using System.Security.Claims;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public partial class AccountService : IAccountService
    {
        private readonly IAuthenticationBroker authenticationBroker;

        public AccountService(IAuthenticationBroker authenticationBroker)
        {
            this.authenticationBroker = authenticationBroker;
        }

        public Account RetrieveAccount()
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = authenticationBroker.GetClaimsPrincipal();
                return MapClaimsPrincipalToAccount(claimsPrincipal);
            } catch (NotAuthenticatedException)
            {
                throw new AccountNotAuthenticatedException();
            }            
        }

        public async ValueTask SignOutAccountAsync()
        {
            await authenticationBroker.SignOutAsync();
        }

        public async ValueTask ChallengeExternalLoginAsync(ChallengeExternalLoginParams @params)
        {
            await authenticationBroker.ChallengeExternalLoginAsync(@params.Provider, @params.RedirectUrl);
        }

        public async ValueTask<ExternalLogin> RetrieveExternalLoginAsync()
        {
            try
            {
                ExternalLoginInfo externalLoginInfo = await authenticationBroker.GetExternalLoginInfoAsync();
                return MapExternalLoginInfoToExternalLogin(externalLoginInfo);
            } catch (ExternalNotAuthenticatedException)
            {
                throw new ExternalLoginNotFoundException();
            }
        }

        public async ValueTask SignInAccountAsync(SignInAccountParams @params)
        {
            Dictionary<string, object> claims = MapAccountToDictionary(@params.Account);
            await authenticationBroker.SignInAsync(claims, @params.IsPersistent, @params.AuthenticationMethod);
        }
    }
}
