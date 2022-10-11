using FMFT.Extensions.TheStandard;
using FMFT.Web.Server.Brokers.Authentications;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.Accounts.Params;

namespace FMFT.Web.Server.Services.Foundations.Accounts
{
    public partial class AccountService : TheStandardService, IAccountService
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

                Account account = authenticationBroker.GetTokenPayload<Account>();
                return ValueTask.FromResult(account);
            });

        public ValueTask<string> CreateTokenAsync(CreateTokenParams @params)
            => TryCatch(() =>
            {
                string token = authenticationBroker.CreateToken(@params.Account);
                return ValueTask.FromResult(token);
            });
    }
}
