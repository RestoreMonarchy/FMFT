using FMFT.Web.Server.Brokers.Encryptions;
using FMFT.Web.Server.Services.Foundations.Users;
using FMFT.Web.Shared.Enums;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Arguments;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Params;
using FMFT.Web.Server.Brokers.Validations;

namespace FMFT.Web.Server.Services.Processings.Users
{
    public partial class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IEncryptionBroker encryptionBroker;

        public UserProcessingService(IUserService userService, IEncryptionBroker encryptionBroker)
        {
            this.userService = userService;
            this.encryptionBroker = encryptionBroker;
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            return await userService.RetrieveUserByIdAsync(userId);
        }

        public async ValueTask<User> RetrieveUserByLoginAsync(string providerName, string providerKey)
        {
            return await userService.RetrieveUserByLoginAsync(providerName, providerKey);
        }

        public async ValueTask<User> RetrieveUserByEmailAsync(string email)
        {
            return await userService.RetrieveUserByEmailAsync(email);
        }

        public async ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
        {
            return await userService.RetrieveAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByEmailAndPasswordAsync(string email, string passwordText)
        {
            User user = await RetrieveUserByEmailAsync(email);

            if (!encryptionBroker.VerifyPassword(passwordText, user.PasswordHash))
            {
                throw new UserPasswordNotMatchException();
            }

            return user;
        }

        public async ValueTask<User> RegisterUserWithPasswordAsync(RegisterUserWithPasswordArguments args)
        {
            return await userService.RegisterUserWithPasswordAsync(args);
        }

        public async ValueTask<User> RegisterUserWithLoginAsync(RegisterUserWithLoginParams @params)
        {
            return await userService.RegisterUserWithLoginAsync(@params);
        }

        public async ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params)
        {
            await userService.UpdateUserRoleAsync(@params);
        }

        public async ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params)
        {
            await userService.UpdateUserCultureAsync(@params);
        }
    }
}
