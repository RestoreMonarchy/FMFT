using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Brokers.Loggings;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Services.Foundations.Users;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IEncryptionBroker encryptionBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserProcessingService(IUserService userService, IEncryptionBroker encryptionBroker, ILoggingBroker loggingBroker)
        {
            this.userService = userService;
            this.encryptionBroker = encryptionBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<User> RetrieveUserByIdAsync(int userId)
            => TryCatch(async () =>
            {
                return await userService.RetrieveUserByIdAsync(userId);
            });

        public ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey)
            => TryCatch(async () =>
            {
                return await userService.RetrieveUserByLoginAsync(providerName, providerKey);
            });

        public ValueTask<User> RetrieveUserByEmailAsync(string email)
            => TryCatch(async () =>
            {
                return await userService.RetrieveUserByEmailAsync(email);
            });

        public ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
            => TryCatch(async () =>
            {
                return await userService.RetrieveAllUsersAsync();
            });

        public ValueTask<User> RetrieveUserByEmailAndPasswordAsync(string email, string passwordText)
            => TryCatch(async () =>
            {
                User user = await RetrieveUserByEmailAsync(email);

                if (!encryptionBroker.VerifyPassword(passwordText, user.PasswordHash))
                {
                    throw new NotMatchPasswordUserProcessingException();
                }

                return user;
            });

        public ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordProcessingParams args)
            => TryCatch(async () =>
            {
                return await userService.RegisterUserWithPasswordAsync(args);
            });

        public ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params)
            => TryCatch(async () =>
            {
                return await userService.RegisterUserWithLoginAsync(@params);
            });

        public ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
            => TryCatch(async () => 
            {
                await userService.UpdateUserRoleAsync(@params);
            });

        public ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
            => TryCatch(async () =>
            {
                await userService.UpdateUserCultureAsync(@params);
            });
    }
}
